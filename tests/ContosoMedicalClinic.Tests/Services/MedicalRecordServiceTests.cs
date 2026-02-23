using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class MedicalRecordServiceTests
{
    private static (MedicalRecordService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new MedicalRecordService(dab), handler);
    }

    private static MedicalRecordDto SampleRecord(int id = 1, int patientId = 1) =>
        new(id, patientId, "A+", "Penicillin", "Lisinopril 10mg", "Hypertension", "Father: heart disease");

    [Fact]
    public async Task GetRecordByPatientAsync_Found_ReturnsFirst()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<MedicalRecordDto> { SampleRecord(1, 1) });

        var result = await svc.GetRecordByPatientAsync(1);

        Assert.NotNull(result);
        Assert.Equal("A+", result.BloodType);
    }

    [Fact]
    public async Task GetRecordByPatientAsync_NotFound_ReturnsNull()
    {
        var (svc, handler) = Create();
        handler.QueueEmptyList();

        var result = await svc.GetRecordByPatientAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpsertRecordAsync_ExistingRecord_UpdatesViaPatch()
    {
        var (svc, handler) = Create();
        // GetRecordByPatientAsync call — returns existing
        handler.QueueDabList(new List<MedicalRecordDto> { SampleRecord(5, 1) });
        // UpdateAsync call
        handler.QueueJsonResponse(SampleRecord(5, 1));

        var result = await svc.UpsertRecordAsync(SampleRecord(0, 1));

        // Second request should be PATCH (update)
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal(HttpMethod.Patch, handler.Requests[1].Method);
    }

    [Fact]
    public async Task UpsertRecordAsync_NoExistingRecord_CreatesViaPost()
    {
        var (svc, handler) = Create();
        // GetRecordByPatientAsync call — returns empty
        handler.QueueEmptyList();
        // CreateAsync call
        handler.QueueJsonResponse(SampleRecord(10, 5));

        var result = await svc.UpsertRecordAsync(SampleRecord(0, 5));

        // Second request should be POST (create)
        Assert.Equal(2, handler.Requests.Count);
        Assert.Equal(HttpMethod.Post, handler.Requests[1].Method);
    }

    [Fact]
    public async Task GetVisitNotesByPatientAsync_ReturnsNotes()
    {
        var (svc, handler) = Create();
        var notes = new List<VisitNoteDto>
        {
            new(1, 1, 1, 1, "Headache", "Migraine", "Rest", "Ibuprofen", "Follow up"),
            new(2, 2, 1, 2, "Chest pain", "Angina", "Medication", "Nitro", "Cardio referral")
        };
        handler.QueueDabList(notes);

        var result = await svc.GetVisitNotesByPatientAsync(1);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task CreateVisitNoteAsync_Success()
    {
        var (svc, handler) = Create();
        var note = new VisitNoteDto(10, 1, 1, 1, "Cough", "Bronchitis", "Antibiotics", "Amoxicillin", "2 weeks");
        handler.QueueJsonResponse(note);

        var result = await svc.CreateVisitNoteAsync(note);

        Assert.Equal(10, result.VisitNoteId);
        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }
}
