using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class StaffServiceTests
{
    private static (StaffService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new StaffService(dab), handler);
    }

    private static StaffDto SampleStaff(int id = 1) =>
        new(id, "Sarah", "Chen", $"sarah{id}@test.com", "555-0101", "Doctor", "General Medicine", "MD-001", "2019-03-15", true);

    private static ShiftDto SampleShift(int id = 1) =>
        new(id, 1, "2026-02-20", "08:00", "16:00", "Morning", null);

    [Fact]
    public async Task GetStaffAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<StaffDto> { SampleStaff(1), SampleStaff(2) });

        var result = await svc.GetStaffAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetStaffMemberAsync_Found()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleStaff(3));

        var result = await svc.GetStaffMemberAsync(3);

        Assert.NotNull(result);
        Assert.Equal(3, result.StaffId);
    }

    [Fact]
    public async Task CreateStaffAsync_UsesPost()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleStaff(10));

        await svc.CreateStaffAsync(SampleStaff(0));

        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task UpdateStaffAsync_UsesPatch()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleStaff(1));

        await svc.UpdateStaffAsync(1, SampleStaff(1));

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
    }

    [Fact]
    public async Task GetShiftsAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<ShiftDto> { SampleShift(1), SampleShift(2) });

        var result = await svc.GetShiftsAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetShiftsByStaffAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<ShiftDto> { SampleShift() });

        await svc.GetShiftsByStaffAsync(3);

        Assert.Contains("StaffId%20eq%203", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task CreateShiftAsync_UsesPost()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleShift(10));

        await svc.CreateShiftAsync(SampleShift(0));

        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task UpdateShiftAsync_UsesPatch()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleShift(1));

        await svc.UpdateShiftAsync(1, SampleShift(1));

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
    }

    [Fact]
    public async Task DeleteShiftAsync_UsesDelete()
    {
        var (svc, handler) = Create();
        handler.QueueResponse(System.Net.HttpStatusCode.NoContent);

        await svc.DeleteShiftAsync(1);

        Assert.Equal(HttpMethod.Delete, handler.LastRequest.Method);
    }
}
