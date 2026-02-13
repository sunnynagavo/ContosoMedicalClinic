using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
var config = app.Services.GetRequiredService<IConfiguration>();
var connectionString = config.GetConnectionString("ContosoMedicalClinicDb");

if (string.IsNullOrEmpty(connectionString))
{
    logger.LogError("No connection string found for ContosoMedicalClinicDb");
    return 1;
}

try
{
    logger.LogInformation("Starting database initialization...");
    logger.LogInformation("Connection string target: {Target}",
        new SqlConnectionStringBuilder(connectionString).DataSource);

    // Step 1: Connect to master and ensure the database exists
    var masterConnStr = new SqlConnectionStringBuilder(connectionString)
    {
        InitialCatalog = "master"
    }.ConnectionString;

    await WaitForSqlServer(masterConnStr, logger);

    var dbName = new SqlConnectionStringBuilder(connectionString).InitialCatalog;
    await using (var masterConn = new SqlConnection(masterConnStr))
    {
        await masterConn.OpenAsync();
        var checkDbCmd = new SqlCommand(
            $"IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = @db) CREATE DATABASE [{dbName}]",
            masterConn);
        checkDbCmd.Parameters.AddWithValue("@db", dbName);
        await checkDbCmd.ExecuteNonQueryAsync();
        logger.LogInformation("Database '{DbName}' ensured", dbName);
    }

    // Wait for the database to be fully online
    logger.LogInformation("Waiting for database to come online...");
    await Task.Delay(5000);
    await WaitForSqlServer(connectionString, logger);

    // Step 2: Connect and run scripts
    await using var conn = new SqlConnection(connectionString);
    await conn.OpenAsync();
    logger.LogInformation("Connected to {DbName}", dbName);

    // Check if schema already exists
    await using var checkCmd = new SqlCommand(
        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Patients'", conn);
    var tableCount = (int)(await checkCmd.ExecuteScalarAsync() ?? 0);

    if (tableCount > 0)
    {
        logger.LogInformation("Database already initialized — skipping. Exiting.");
        return 0;
    }

    // List embedded resources for diagnostics
    var resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
    logger.LogInformation("Embedded resources: {Resources}", string.Join(", ", resourceNames));

    // Run schema script
    logger.LogInformation("Running schema script...");
    var schemaScript = ReadEmbeddedResource("001_CreateSchema.sql");
    await ExecuteScript(conn, schemaScript, logger);
    logger.LogInformation("Schema created successfully");

    // Run seed data script
    logger.LogInformation("Running seed data script...");
    var seedScript = ReadEmbeddedResource("002_SeedData.sql");
    await ExecuteScript(conn, seedScript, logger);
    logger.LogInformation("Seed data inserted successfully");

    // Verify
    await using var verifyCmd = new SqlCommand(
        "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo'", conn);
    var verifiedCount = (int)(await verifyCmd.ExecuteScalarAsync() ?? 0);
    logger.LogInformation("Database initialization complete. {Count} tables created. Exiting.", verifiedCount);

    return 0;
}
catch (Exception ex)
{
    logger.LogError(ex, "Database initialization failed");
    return 1;
}

static async Task WaitForSqlServer(string connectionString, ILogger logger)
{
    const int maxRetries = 30;
    for (int i = 0; i < maxRetries; i++)
    {
        try
        {
            await using var conn = new SqlConnection(connectionString);
            await conn.OpenAsync();
            logger.LogInformation("SQL Server ready on attempt {Attempt}", i + 1);
            return;
        }
        catch (SqlException) when (i < maxRetries - 1)
        {
            logger.LogWarning("SQL Server not ready (attempt {Attempt}/{Max}), retrying...", i + 1, maxRetries);
            await Task.Delay(2000);
        }
    }
    throw new TimeoutException("Could not connect to SQL Server after max retries");
}

static string ReadEmbeddedResource(string name)
{
    var assembly = Assembly.GetExecutingAssembly();
    var resourceNames = assembly.GetManifestResourceNames();

    var stream = assembly.GetManifestResourceStream(name);
    if (stream is null)
    {
        var match = resourceNames.FirstOrDefault(r => r.EndsWith(name));
        if (match is not null)
            stream = assembly.GetManifestResourceStream(match);
    }

    if (stream is null)
        throw new FileNotFoundException($"Embedded resource '{name}' not found. Available: {string.Join(", ", resourceNames)}");

    using var reader = new StreamReader(stream);
    return reader.ReadToEnd();
}

static async Task ExecuteScript(SqlConnection conn, string script, ILogger logger)
{
    var batches = System.Text.RegularExpressions.Regex.Split(script, @"^\s*GO\s*$",
        System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.IgnoreCase);

    foreach (var batch in batches)
    {
        var trimmed = batch.Trim();
        if (string.IsNullOrEmpty(trimmed)) continue;

        try
        {
            await using var cmd = new SqlCommand(trimmed, conn);
            cmd.CommandTimeout = 60;
            await cmd.ExecuteNonQueryAsync();
        }
        catch (SqlException ex)
        {
            logger.LogWarning("Batch warning: {Msg}", ex.Message);
        }
    }
}
