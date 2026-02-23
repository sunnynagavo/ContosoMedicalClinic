using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class PatientServiceTests
{
    private static (PatientService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new PatientService(dab), handler);
    }

    private static PatientDto SamplePatient(int id = 1) =>
        new(id, "John", "Smith", "1985-04-12", "Male", $"patient{id}@test.com",
            "555-0001", "100 Main St", "City", "ST", "12345", "Jane Smith", "555-0002", true);

    [Fact]
    public async Task GetPatientsAsync_ReturnsAll()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<PatientDto> { SamplePatient(1), SamplePatient(2) });

        var result = await svc.GetPatientsAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetPatientAsync_Found()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SamplePatient(3));

        var result = await svc.GetPatientAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.PatientId);
    }

    [Fact]
    public async Task GetPatientAsync_NotFound()
    {
        var (svc, handler) = Create();
        handler.QueueNotFound();

        var result = await svc.GetPatientAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreatePatientAsync_UsesPost()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SamplePatient(10));

        await svc.CreatePatientAsync(SamplePatient(0));

        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
        Assert.Equal("/api/Patient", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task UpdatePatientAsync_UsesPatch()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SamplePatient(1));

        await svc.UpdatePatientAsync(1, SamplePatient(1));

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
        Assert.Contains("/PatientId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }

    [Fact]
    public async Task DeletePatientAsync_UsesDelete()
    {
        var (svc, handler) = Create();
        handler.QueueResponse(System.Net.HttpStatusCode.NoContent);

        await svc.DeletePatientAsync(1);

        Assert.Equal(HttpMethod.Delete, handler.LastRequest.Method);
        Assert.Contains("/PatientId/1", handler.LastRequest.RequestUri!.AbsolutePath);
    }
}
