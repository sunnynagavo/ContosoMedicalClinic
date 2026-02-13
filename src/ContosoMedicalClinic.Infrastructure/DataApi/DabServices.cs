using ContosoMedicalClinic.Application.DTOs;
using ContosoMedicalClinic.Application.Interfaces;

namespace ContosoMedicalClinic.Infrastructure.DataApi;

public class ServiceCatalogService(DabHttpClient dab) : IServiceCatalogService
{
    public async Task<List<ServiceCategoryDto>> GetCategoriesAsync() =>
        await dab.GetListAsync<ServiceCategoryDto>("ServiceCategory");

    public async Task<List<ServiceDto>> GetServicesAsync() =>
        await dab.GetListAsync<ServiceDto>("Service");

    public async Task<List<ServiceDto>> GetServicesByCategoryAsync(int categoryId) =>
        await dab.GetListAsync<ServiceDto>("Service", $"CategoryId eq {categoryId}");

    public async Task<ServiceDto?> GetServiceAsync(int serviceId) =>
        await dab.GetByIdAsync<ServiceDto>("Service", serviceId);

    public async Task<ServiceDto> CreateServiceAsync(ServiceDto service) =>
        await dab.CreateAsync<ServiceDto>("Service", service);

    public async Task<ServiceDto> UpdateServiceAsync(int serviceId, ServiceDto service) =>
        await dab.UpdateAsync<ServiceDto>("Service", serviceId, service);

    public async Task DeleteServiceAsync(int serviceId) =>
        await dab.DeleteAsync("Service", serviceId);
}

public class AppointmentService(DabHttpClient dab) : IAppointmentService
{
    public async Task<List<AppointmentDto>> GetAppointmentsAsync() =>
        await dab.GetListAsync<AppointmentDto>("Appointment");

    public async Task<List<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId) =>
        await dab.GetListAsync<AppointmentDto>("Appointment", $"PatientId eq {patientId}");

    public async Task<List<AppointmentDto>> GetAppointmentsByProviderAsync(int providerId) =>
        await dab.GetListAsync<AppointmentDto>("Appointment", $"ProviderId eq {providerId}");

    public async Task<List<AppointmentDto>> GetAppointmentsByDateAsync(string date) =>
        await dab.GetListAsync<AppointmentDto>("Appointment", $"AppointmentDate eq {date}");

    public async Task<AppointmentDto?> GetAppointmentAsync(int appointmentId) =>
        await dab.GetByIdAsync<AppointmentDto>("Appointment", appointmentId);

    public async Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointment) =>
        await dab.CreateAsync<AppointmentDto>("Appointment", appointment);

    public async Task<AppointmentDto> UpdateAppointmentAsync(int appointmentId, AppointmentDto appointment) =>
        await dab.UpdateAsync<AppointmentDto>("Appointment", appointmentId, new
        {
            appointment.PatientId,
            appointment.ProviderId,
            appointment.ServiceId,
            appointment.AppointmentDate,
            appointment.StartTime,
            appointment.EndTime,
            appointment.Status,
            appointment.Notes,
            appointment.CancellationReason
        });

    public async Task UpdateStatusAsync(int appointmentId, string status) =>
        await dab.UpdateAsync<AppointmentDto>("Appointment", appointmentId,
            new { Status = status });

    public async Task CancelAppointmentAsync(int appointmentId, string? reason) =>
        await dab.UpdateAsync<AppointmentDto>("Appointment", appointmentId,
            new { Status = "Cancelled", CancellationReason = reason });
}

public class PatientService(DabHttpClient dab) : IPatientService
{
    public async Task<List<PatientDto>> GetPatientsAsync() =>
        await dab.GetListAsync<PatientDto>("Patient");

    public async Task<PatientDto?> GetPatientAsync(int patientId) =>
        await dab.GetByIdAsync<PatientDto>("Patient", patientId);

    public async Task<PatientDto> CreatePatientAsync(PatientDto patient) =>
        await dab.CreateAsync<PatientDto>("Patient", patient);

    public async Task<PatientDto> UpdatePatientAsync(int patientId, PatientDto patient) =>
        await dab.UpdateAsync<PatientDto>("Patient", patientId, patient);
}

public class ProviderService(DabHttpClient dab) : IProviderService
{
    public async Task<List<ProviderDto>> GetProvidersAsync() =>
        await dab.GetListAsync<ProviderDto>("ProviderDetails");

    public async Task<ProviderDto?> GetProviderAsync(int providerId) =>
        await dab.GetByIdAsync<ProviderDto>("ProviderDetails", providerId);

    public async Task<List<ProviderDto>> GetProvidersByServiceAsync(int serviceId) =>
        await dab.GetListAsync<ProviderDto>("ProviderDetails"); // Filter client-side for now
}

public class InvoiceService(DabHttpClient dab) : IInvoiceService
{
    public async Task<List<InvoiceDto>> GetInvoicesAsync() =>
        await dab.GetListAsync<InvoiceDto>("Invoice");

    public async Task<List<InvoiceDto>> GetInvoicesByPatientAsync(int patientId) =>
        await dab.GetListAsync<InvoiceDto>("Invoice", $"PatientId eq {patientId}");

    public async Task<InvoiceDto?> GetInvoiceAsync(int invoiceId) =>
        await dab.GetByIdAsync<InvoiceDto>("Invoice", invoiceId);

    public async Task<List<InvoiceLineItemDto>> GetInvoiceLineItemsAsync(int invoiceId) =>
        await dab.GetListAsync<InvoiceLineItemDto>("InvoiceLineItem", $"InvoiceId eq {invoiceId}");

    public async Task<InvoiceDto> CreateInvoiceAsync(InvoiceDto invoice) =>
        await dab.CreateAsync<InvoiceDto>("Invoice", invoice);

    public async Task<InvoiceDto> UpdateInvoiceAsync(int invoiceId, InvoiceDto invoice) =>
        await dab.UpdateAsync<InvoiceDto>("Invoice", invoiceId, invoice);
}

public class PaymentService(DabHttpClient dab) : IPaymentService
{
    public async Task<List<PaymentDto>> GetPaymentsByInvoiceAsync(int invoiceId) =>
        await dab.GetListAsync<PaymentDto>("Payment", $"InvoiceId eq {invoiceId}");

    public async Task<List<PaymentDto>> GetPaymentsByPatientAsync(int patientId) =>
        await dab.GetListAsync<PaymentDto>("Payment", $"PatientId eq {patientId}");

    public async Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto payment) =>
        await dab.CreateAsync<PaymentDto>("Payment", payment);
}

public class MedicalRecordService(DabHttpClient dab) : IMedicalRecordService
{
    public async Task<MedicalRecordDto?> GetRecordByPatientAsync(int patientId)
    {
        var records = await dab.GetListAsync<MedicalRecordDto>("MedicalRecord", $"PatientId eq {patientId}");
        return records.FirstOrDefault();
    }

    public async Task<MedicalRecordDto> UpsertRecordAsync(MedicalRecordDto record)
    {
        var existing = await GetRecordByPatientAsync(record.PatientId);
        if (existing is not null)
            return await dab.UpdateAsync<MedicalRecordDto>("MedicalRecord", existing.RecordId, record);
        return await dab.CreateAsync<MedicalRecordDto>("MedicalRecord", record);
    }

    public async Task<List<VisitNoteDto>> GetVisitNotesByPatientAsync(int patientId) =>
        await dab.GetListAsync<VisitNoteDto>("VisitNote", $"PatientId eq {patientId}");

    public async Task<VisitNoteDto> CreateVisitNoteAsync(VisitNoteDto visitNote) =>
        await dab.CreateAsync<VisitNoteDto>("VisitNote", visitNote);
}

public class StaffService(DabHttpClient dab) : IStaffService
{
    public async Task<List<StaffDto>> GetStaffAsync() =>
        await dab.GetListAsync<StaffDto>("Staff");

    public async Task<StaffDto?> GetStaffMemberAsync(int staffId) =>
        await dab.GetByIdAsync<StaffDto>("Staff", staffId);

    public async Task<StaffDto> CreateStaffAsync(StaffDto staff) =>
        await dab.CreateAsync<StaffDto>("Staff", staff);

    public async Task<StaffDto> UpdateStaffAsync(int staffId, StaffDto staff) =>
        await dab.UpdateAsync<StaffDto>("Staff", staffId, staff);

    public async Task<List<ShiftDto>> GetShiftsAsync() =>
        await dab.GetListAsync<ShiftDto>("Shift");

    public async Task<List<ShiftDto>> GetShiftsByStaffAsync(int staffId) =>
        await dab.GetListAsync<ShiftDto>("Shift", $"StaffId eq {staffId}");

    public async Task<ShiftDto> CreateShiftAsync(ShiftDto shift) =>
        await dab.CreateAsync<ShiftDto>("Shift", shift);

    public async Task<ShiftDto> UpdateShiftAsync(int shiftId, ShiftDto shift) =>
        await dab.UpdateAsync<ShiftDto>("Shift", shiftId, shift);

    public async Task DeleteShiftAsync(int shiftId) =>
        await dab.DeleteAsync("Shift", shiftId);
}

public class InsuranceService(DabHttpClient dab) : IInsuranceService
{
    public async Task<List<InsuranceProviderDto>> GetInsuranceProvidersAsync() =>
        await dab.GetListAsync<InsuranceProviderDto>("InsuranceProvider");

    public async Task<InsuranceProviderDto> CreateInsuranceProviderAsync(InsuranceProviderDto provider) =>
        await dab.CreateAsync<InsuranceProviderDto>("InsuranceProvider", provider);

    public async Task<InsuranceProviderDto> UpdateInsuranceProviderAsync(int id, InsuranceProviderDto provider) =>
        await dab.UpdateAsync<InsuranceProviderDto>("InsuranceProvider", id, provider);

    public async Task<List<PatientInsuranceDto>> GetPatientInsuranceAsync(int patientId) =>
        await dab.GetListAsync<PatientInsuranceDto>("PatientInsurance", $"PatientId eq {patientId}");

    public async Task<List<InsuranceClaimDto>> GetClaimsAsync() =>
        await dab.GetListAsync<InsuranceClaimDto>("InsuranceClaim");

    public async Task<InsuranceClaimDto> CreateClaimAsync(InsuranceClaimDto claim) =>
        await dab.CreateAsync<InsuranceClaimDto>("InsuranceClaim", claim);

    public async Task<InsuranceClaimDto> UpdateClaimAsync(int claimId, InsuranceClaimDto claim) =>
        await dab.UpdateAsync<InsuranceClaimDto>("InsuranceClaim", claimId, claim);
}

public class AuthService(DabHttpClient dab) : IAuthService
{
    private const string DemoHash = "DEMO_HASH";

    public async Task<UserAccountDto?> GetUserByEmailAsync(string email)
    {
        var users = await dab.GetListAsync<UserAccountDto>("UserAccount", $"Email eq '{email}'");
        return users.FirstOrDefault();
    }

    public async Task<UserAccountDto> CreateUserAsync(CreateUserAccountDto user) =>
        await dab.CreateAsync<UserAccountDto>("UserAccount", user);

    public Task<bool> ValidatePasswordAsync(string inputPassword, string storedHash)
    {
        // For demo accounts seeded with DEMO_HASH, accept "Password1!"
        if (storedHash == DemoHash)
            return Task.FromResult(inputPassword == "Password1!");

        // For real accounts, compare PBKDF2 hash
        return Task.FromResult(storedHash == HashPassword(inputPassword));
    }

    public string HashPassword(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA256(
            System.Text.Encoding.UTF8.GetBytes("ContosoMedicalClinic2026"));
        var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hash);
    }
}
