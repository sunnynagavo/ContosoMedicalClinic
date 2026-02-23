using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class InsuranceServiceTests
{
    private static (InsuranceService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new InsuranceService(dab), handler);
    }

    private static InsuranceProviderDto SampleProvider(int id = 1) =>
        new(id, "Contoso Health", "800-555-0001", "claims@contoso.com", "https://contoso.com", true);

    [Fact]
    public async Task GetInsuranceProvidersAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<InsuranceProviderDto> { SampleProvider(1), SampleProvider(2) });

        var result = await svc.GetInsuranceProvidersAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateInsuranceProviderAsync_UsesPost()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleProvider(10));

        await svc.CreateInsuranceProviderAsync(SampleProvider(0));

        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task UpdateInsuranceProviderAsync_UsesPatch()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleProvider(1));

        await svc.UpdateInsuranceProviderAsync(1, SampleProvider(1));

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
        Assert.Contains("/InsuranceProviderId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task DeleteInsuranceProviderAsync_UsesDelete()
    {
        var (svc, handler) = Create();
        handler.QueueResponse(System.Net.HttpStatusCode.NoContent);

        await svc.DeleteInsuranceProviderAsync(1);

        Assert.Equal(HttpMethod.Delete, handler.LastRequest.Method);
        Assert.Contains("/InsuranceProviderId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task GetPatientInsuranceAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<PatientInsuranceDto>
        {
            new(1, 1, 1, "CH-100001", "GRP-A", "John Smith", "2025-01-01", "2026-12-31", true, true)
        });

        var result = await svc.GetPatientInsuranceAsync(1);

        Assert.Single(result);
        Assert.Contains("PatientId%20eq%201", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task GetClaimsAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<InsuranceClaimDto>
        {
            new(1, 1, 1, "CLM-001", "2026-01-11", 150m, 50m, "Approved", null, "2026-01-20")
        });

        var result = await svc.GetClaimsAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task CreateClaimAsync_UsesPost()
    {
        var (svc, handler) = Create();
        var claim = new InsuranceClaimDto(0, 1, 1, "CLM-NEW", "2026-03-01", 200m, null, "Submitted", null, null);
        handler.QueueJsonResponse(claim with { ClaimId = 10 });

        var result = await svc.CreateClaimAsync(claim);

        Assert.Equal(10, result.ClaimId);
        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task UpdateClaimAsync_UsesPatch()
    {
        var (svc, handler) = Create();
        var claim = new InsuranceClaimDto(1, 1, 1, "CLM-001", "2026-01-11", 150m, 50m, "Approved", null, "2026-01-20");
        handler.QueueJsonResponse(claim);

        await svc.UpdateClaimAsync(1, claim);

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
        Assert.Contains("/ClaimId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }
}
