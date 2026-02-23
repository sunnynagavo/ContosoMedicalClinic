using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class AppointmentServiceTests
{
    private static (AppointmentService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new AppointmentService(dab), handler);
    }

    private static AppointmentDto SampleAppointment(int id = 1, string status = "Scheduled") =>
        new(id, 1, 1, 1, "2026-03-01", "09:00", "09:30", status, "Notes", null);

    [Fact]
    public async Task GetAppointmentsAsync_ReturnsAll()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<AppointmentDto> { SampleAppointment(1), SampleAppointment(2) });

        var result = await svc.GetAppointmentsAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetAppointmentsByPatientAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<AppointmentDto> { SampleAppointment() });

        await svc.GetAppointmentsByPatientAsync(5);

        Assert.Contains("PatientId%20eq%205", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task GetAppointmentsByDateAsync_ValidDate_QueriesDab()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<AppointmentDto> { SampleAppointment() });

        var result = await svc.GetAppointmentsByDateAsync("2026-03-01");

        Assert.Single(result);
        Assert.Contains("AppointmentDate", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task GetAppointmentsByDateAsync_InvalidDate_ReturnsEmpty()
    {
        var (svc, handler) = Create();

        var result = await svc.GetAppointmentsByDateAsync("not-a-date");

        Assert.Empty(result);
        Assert.Empty(handler.Requests); // No HTTP call should be made
    }

    [Fact]
    public async Task GetAppointmentsByDateAsync_WrongFormat_ReturnsEmpty()
    {
        var (svc, handler) = Create();

        var result = await svc.GetAppointmentsByDateAsync("03/01/2026");

        Assert.Empty(result);
        Assert.Empty(handler.Requests);
    }

    [Fact]
    public async Task GetAppointmentAsync_Found_ReturnsAppointment()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleAppointment(5));

        var result = await svc.GetAppointmentAsync(5);

        Assert.NotNull(result);
        Assert.Equal(5, result.AppointmentId);
    }

    [Fact]
    public async Task GetAppointmentAsync_NotFound_ReturnsNull()
    {
        var (svc, handler) = Create();
        handler.QueueNotFound();

        var result = await svc.GetAppointmentAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAppointmentAsync_SendsCorrectData()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleAppointment(10));
        var dto = new CreateAppointmentDto(1, 2, 3, "2026-04-01", "10:00", "10:30");

        var result = await svc.CreateAppointmentAsync(dto);

        Assert.Equal(10, result.AppointmentId);
        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task CancelAppointmentAsync_SetsStatusCancelled()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleAppointment(1, "Cancelled"));

        await svc.CancelAppointmentAsync(1, "Patient request");

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
        var body = await handler.LastRequest.Content!.ReadAsStringAsync();
        Assert.Contains("Cancelled", body);
        Assert.Contains("Patient request", body);
    }

    [Fact]
    public async Task RescheduleAsync_UpdatesDateTimeAndStatus()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleAppointment(1));

        await svc.RescheduleAsync(1, "2026-05-01", "14:00", "14:30");

        var body = await handler.LastRequest.Content!.ReadAsStringAsync();
        Assert.Contains("2026-05-01", body);
        Assert.Contains("14:00", body);
        Assert.Contains("14:30", body);
        Assert.Contains("Scheduled", body);
    }

    [Fact]
    public async Task ResumeAppointmentAsync_ResetsToScheduled()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleAppointment(1));

        await svc.ResumeAppointmentAsync(1);

        var body = await handler.LastRequest.Content!.ReadAsStringAsync();
        Assert.Contains("Scheduled", body);
    }

    [Fact]
    public async Task UpdateStatusAsync_SendsNewStatus()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleAppointment(1, "Confirmed"));

        await svc.UpdateStatusAsync(1, "Confirmed");

        var body = await handler.LastRequest.Content!.ReadAsStringAsync();
        Assert.Contains("Confirmed", body);
    }
}
