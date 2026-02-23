using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class ProviderServiceTests
{
    private static (ProviderService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new ProviderService(dab), handler);
    }

    private static ProviderDto SampleProvider(int id, string firstName) =>
        new(id, id, "Dr.", "Bio", null, true, true, firstName, "Last", $"{firstName}@test.com", null, "General", "LIC-001");

    [Fact]
    public async Task GetProvidersAsync_ReturnsAll()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<ProviderDto> { SampleProvider(1, "Sarah"), SampleProvider(2, "James") });

        var result = await svc.GetProvidersAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetProviderAsync_Found_ReturnsProvider()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleProvider(1, "Sarah"));

        var result = await svc.GetProviderAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Sarah", result.FirstName);
    }

    [Fact]
    public async Task GetProviderAsync_NotFound_ReturnsNull()
    {
        var (svc, handler) = Create();
        handler.QueueNotFound();

        var result = await svc.GetProviderAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetProvidersByServiceAsync_FiltersUsingProviderServices()
    {
        var (svc, handler) = Create();

        // First call: Get ProviderService mappings for serviceId=5
        handler.QueueDabList(new List<ProviderServiceDto>
        {
            new(1, 5),
            new(3, 5)
        });

        // Second call: Get all providers from ProviderDetails view
        handler.QueueDabList(new List<ProviderDto>
        {
            SampleProvider(1, "Sarah"),   // has service 5
            SampleProvider(2, "James"),   // doesn't have service 5
            SampleProvider(3, "Maria"),   // has service 5
            SampleProvider(4, "Michael")  // doesn't have service 5
        });

        var result = await svc.GetProvidersByServiceAsync(5);

        Assert.Equal(2, result.Count);
        Assert.Contains(result, p => p.FirstName == "Sarah");
        Assert.Contains(result, p => p.FirstName == "Maria");
        Assert.DoesNotContain(result, p => p.FirstName == "James");
    }

    [Fact]
    public async Task GetProvidersByServiceAsync_NoMappings_ReturnsEmpty()
    {
        var (svc, handler) = Create();
        handler.QueueEmptyList(); // No ProviderService mappings
        handler.QueueDabList(new List<ProviderDto> { SampleProvider(1, "Sarah") }); // Providers exist

        var result = await svc.GetProvidersByServiceAsync(999);

        Assert.Empty(result);
    }
}
