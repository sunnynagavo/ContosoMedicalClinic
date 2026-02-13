using ContosoMedicalClinic.Application.Interfaces;
using ContosoMedicalClinic.Infrastructure.DataApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContosoMedicalClinic.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register the generic DAB HTTP client using Aspire service discovery
        // The "data-api-builder" name is resolved by Aspire's service discovery
        services.AddHttpClient<DabHttpClient>(client =>
        {
            client.BaseAddress = new Uri("https+http://data-api-builder");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Register all domain service implementations
        services.AddScoped<IServiceCatalogService, ServiceCatalogService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IProviderService, ProviderService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IMedicalRecordService, MedicalRecordService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IInsuranceService, InsuranceService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
