using CommunityToolkit.Aspire.Hosting.Azure.DataApiBuilder;

var builder = DistributedApplication.CreateBuilder(args);

// 1. SQL Server container + database
var sqlServer = builder.AddSqlServer("sql");
var db = sqlServer.AddDatabase("ContosoMedicalClinicDb");

// 2. DB Initializer — runs schema + seed scripts, then exits
var dbInit = builder.AddProject<Projects.ContosoMedicalClinic_DbInitializer>("db-initializer")
    .WithReference(db)
    .WaitFor(db);

// 3. Data API Builder — waits for DB init to complete (exit code 0)
var dab = builder.AddDataAPIBuilder("data-api-builder")
    .WithReference(db)
    .WaitForCompletion(dbInit);

// 4. Blazor Web Application — waits for DAB to be healthy
var web = builder.AddProject<Projects.ContosoMedicalClinic_Web>("web")
    .WithReference(dab)
    .WaitFor(dab)
    .WithExternalHttpEndpoints();

builder.Build().Run();
