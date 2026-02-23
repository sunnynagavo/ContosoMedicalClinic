using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class InvoiceServiceTests
{
    private static (InvoiceService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new InvoiceService(dab), handler);
    }

    private static InvoiceDto SampleInvoice(int id = 1) =>
        new(id, 1, 1, $"INV-{id:D3}", "2026-01-10", "2026-02-10", 150m, 50m, 100m, 0m, "Paid");

    [Fact]
    public async Task GetInvoicesAsync_ReturnsList()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<InvoiceDto> { SampleInvoice(1), SampleInvoice(2) });

        var result = await svc.GetInvoicesAsync();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public async Task GetInvoicesByPatientAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<InvoiceDto> { SampleInvoice() });

        await svc.GetInvoicesByPatientAsync(3);

        Assert.Contains("PatientId%20eq%203", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task GetInvoiceAsync_Found()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleInvoice(5));

        var result = await svc.GetInvoiceAsync(5);

        Assert.NotNull(result);
        Assert.Equal(5, result.InvoiceId);
    }

    [Fact]
    public async Task GetInvoiceLineItemsAsync_FiltersById()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<InvoiceLineItemDto>
        {
            new(1, 1, 1, "Consultation", 1, 150m, 150m)
        });

        var result = await svc.GetInvoiceLineItemsAsync(1);

        Assert.Single(result);
        Assert.Contains("InvoiceId%20eq%201", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task CreateInvoiceAsync_UsesPost()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleInvoice(10));

        await svc.CreateInvoiceAsync(SampleInvoice(0));

        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }

    [Fact]
    public async Task UpdateInvoiceAsync_UsesPatch()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(SampleInvoice(1));

        await svc.UpdateInvoiceAsync(1, SampleInvoice(1));

        Assert.Equal(HttpMethod.Patch, handler.LastRequest.Method);
    }
}
