using System.Net;
using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests;

public class DabHttpClientTests
{
    [Fact]
    public async Task GetListAsync_WithNoFilter_CallsCorrectUrl()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueDabList(new List<PatientDto>());

        await client.GetListAsync<PatientDto>("Patient");

        Assert.Equal("/api/Patient", handler.LastRequest.RequestUri!.AbsolutePath);
        Assert.Null(handler.LastRequest.RequestUri.Query is "" ? null : handler.LastRequest.RequestUri.Query);
    }

    [Fact]
    public async Task GetListAsync_WithFilter_UrlEncodesFilter()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueDabList(new List<PatientDto>());

        await client.GetListAsync<PatientDto>("Patient", "PatientId eq 1");

        var uri = handler.LastRequest.RequestUri!;
        Assert.Equal("/api/Patient", uri.AbsolutePath);
        Assert.Contains("$filter=", uri.Query);
        Assert.Contains("PatientId%20eq%201", uri.Query);
    }

    [Fact]
    public async Task GetListAsync_ReturnsList()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        var expected = new List<ServiceCategoryDto>
        {
            new(1, "General Medicine", "Primary care", "bi-heart", 1, true),
            new(2, "Dental", "Dental care", "bi-smile", 2, true)
        };
        handler.QueueDabList(expected);

        var result = await client.GetListAsync<ServiceCategoryDto>("ServiceCategory");

        Assert.Equal(2, result.Count);
        Assert.Equal("General Medicine", result[0].Name);
        Assert.Equal("Dental", result[1].Name);
    }

    [Fact]
    public async Task GetListAsync_EmptyResponse_ReturnsEmptyList()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueEmptyList();

        var result = await client.GetListAsync<PatientDto>("Patient");

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_Found_ReturnsEntity()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        var expected = new PatientDto(1, "John", "Smith", "1985-04-12", "Male",
            "john@test.com", "555-0001", "100 Main St", "City", "ST", "12345",
            "Jane Smith", "555-0002", true);
        handler.QueueJsonResponse(expected);

        var result = await client.GetByIdAsync<PatientDto>("Patient", 1, "PatientId");

        Assert.NotNull(result);
        Assert.Equal("John", result.FirstName);
        Assert.Equal("/api/Patient/PatientId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task GetByIdAsync_NotFound_ReturnsDefault()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueNotFound();

        var result = await client.GetByIdAsync<PatientDto>("Patient", 999, "PatientId");

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_Success_ReturnsCreatedEntity()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        var created = new PatientDto(10, "Test", "User", "2000-01-01", null,
            "test@email.com", null, null, null, null, null, null, null, true);
        handler.QueueJsonResponse(created);

        var result = await client.CreateAsync<PatientDto>("Patient", new { FirstName = "Test", LastName = "User" });

        Assert.Equal(10, result.PatientId);
        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
        Assert.Equal("/api/Patient", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task CreateAsync_ServerError_ThrowsWithDetails()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueError(HttpStatusCode.BadRequest, "Validation failed: Name is required");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(
            () => client.CreateAsync<PatientDto>("Patient", new { }));

        Assert.Contains("DAB create on 'Patient' failed (400)", ex.Message);
        Assert.Contains("Validation failed", ex.Message);
    }

    [Fact]
    public async Task UpdateAsync_Success_ReturnsUpdatedEntity()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        var updated = new PatientDto(1, "John", "Updated", "1985-04-12", "Male",
            "john@test.com", null, null, null, null, null, null, null, true);
        handler.QueueJsonResponse(updated);

        var result = await client.UpdateAsync<PatientDto>("Patient", 1,
            new { LastName = "Updated" }, "PatientId");

        Assert.Equal("Updated", result.LastName);
        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
        Assert.Equal("/api/Patient/PatientId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task UpdateAsync_ServerError_ThrowsWithDetails()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueError(HttpStatusCode.NotFound, "Entity not found");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(
            () => client.UpdateAsync<PatientDto>("Patient", 999, new { }, "PatientId"));

        Assert.Contains("DAB update on 'Patient' failed (404)", ex.Message);
    }

    [Fact]
    public async Task DeleteAsync_Success_CompletesWithoutError()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueResponse(HttpStatusCode.NoContent);

        await client.DeleteAsync("Patient", 1, "PatientId");

        Assert.Equal(HttpMethod.Delete, handler.LastRequest.Method);
        Assert.Equal("/api/Patient/PatientId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task DeleteAsync_ServerError_ThrowsWithDetails()
    {
        var (client, handler) = DabTestHelpers.CreateDabClient();
        handler.QueueError(HttpStatusCode.InternalServerError, "FK constraint violation");

        var ex = await Assert.ThrowsAsync<HttpRequestException>(
            () => client.DeleteAsync("Patient", 1, "PatientId"));

        Assert.Contains("DAB delete on 'Patient' failed (500)", ex.Message);
        Assert.Contains("FK constraint", ex.Message);
    }
}
