using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class ServiceCatalogServiceTests
{
    private static (ServiceCatalogService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new ServiceCatalogService(dab), handler);
    }

    [Fact]
    public async Task GetCategoriesAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<ServiceCategoryDto>
        {
            new(1, "General Medicine", "Primary care", "bi-heart", 1, true),
            new(2, "Dental", "Dental care", "bi-smile", 2, true)
        });

        var result = await svc.GetCategoriesAsync();

        Assert.Equal(2, result.Count);
        Assert.Equal("General Medicine", result[0].Name);
    }

    [Fact]
    public async Task GetServicesAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<ServiceDto>
        {
            new(1, 1, "General Consultation", "Standard consultation", 30, 150.00m, "99213", true)
        });

        var result = await svc.GetServicesAsync();

        Assert.Single(result);
        Assert.Equal(150.00m, result[0].DefaultPrice);
    }

    [Fact]
    public async Task GetServicesByCategoryAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<ServiceDto>());

        await svc.GetServicesByCategoryAsync(3);

        Assert.Contains("CategoryId%20eq%203", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task GetServiceAsync_Found()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(new ServiceDto(1, 1, "Consultation", null, 30, 150m, "99213", true));

        var result = await svc.GetServiceAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Consultation", result.Name);
    }

    [Fact]
    public async Task CreateServiceAsync_Success()
    {
        var (svc, handler) = Create();
        var dto = new ServiceDto(0, 1, "New Service", "Desc", 60, 200m, "99999", true);
        handler.QueueJsonResponse(dto with { ServiceId = 10 });

        var result = await svc.CreateServiceAsync(dto);

        Assert.Equal(10, result.ServiceId);
        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task UpdateServiceAsync_Success()
    {
        var (svc, handler) = Create();
        var dto = new ServiceDto(1, 1, "Updated", null, 30, 175m, "99213", true);
        handler.QueueJsonResponse(dto);

        var result = await svc.UpdateServiceAsync(1, dto);

        Assert.Equal("Updated", result.Name);
        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
    }

    [Fact]
    public async Task DeleteServiceAsync_Success()
    {
        var (svc, handler) = Create();
        handler.QueueResponse(System.Net.HttpStatusCode.NoContent);

        await svc.DeleteServiceAsync(5);

        Assert.Equal(HttpMethod.Delete, handler.LastRequest.Method);
        Assert.Contains("/ServiceId/5", handler.LastRequest.RequestUri!.AbsolutePath);
    }
}
