using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Infrastructure.DataApi;
using ContosoMedicalClinic.Tests.Helpers;

namespace ContosoMedicalClinic.Tests.Services;

public class PaymentServiceTests
{
    private static (PaymentService Service, MockHttpMessageHandler Handler) Create()
    {
        var (dab, handler) = DabTestHelpers.CreateDabClient();
        return (new PaymentService(dab), handler);
    }

    [Fact]
    public async Task GetPaymentsByInvoiceAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<PaymentDto>
        {
            new(1, 1, 1, "2026-01-15", 100m, "CreditCard", "PAY-001", null)
        });

        var result = await svc.GetPaymentsByInvoiceAsync(1);

        Assert.Single(result);
        Assert.Contains("InvoiceId%20eq%201", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task GetPaymentsByPatientAsync_FiltersCorrectly()
    {
        var (svc, handler) = Create();
        handler.QueueDabList(new List<PaymentDto>());

        await svc.GetPaymentsByPatientAsync(5);

        Assert.Contains("PatientId%20eq%205", handler.LastRequest.RequestUri!.Query);
    }

    [Fact]
    public async Task CreatePaymentAsync_UsesPost()
    {
        var (svc, handler) = Create();
        handler.QueueJsonResponse(new PaymentDto(10, 1, 1, "2026-03-01", 50m, "Cash", null, null));

        var result = await svc.CreatePaymentAsync(new CreatePaymentDto(1, 1, 50m, "Cash"));

        Assert.Equal(10, result.PaymentId);
        Assert.Equal(HttpMethod.Post, handler.LastRequest.Method);
    }
}
