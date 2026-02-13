using ContosoMedicalClinic.Application.DTOs;

namespace ContosoMedicalClinic.Application.Interfaces;

public interface IServiceCatalogService
{
    Task<List<ServiceCategoryDto>> GetCategoriesAsync();
    Task<List<ServiceDto>> GetServicesAsync();
    Task<List<ServiceDto>> GetServicesByCategoryAsync(int categoryId);
    Task<ServiceDto?> GetServiceAsync(int serviceId);
    Task<ServiceDto> CreateServiceAsync(ServiceDto service);
    Task<ServiceDto> UpdateServiceAsync(int serviceId, ServiceDto service);
    Task DeleteServiceAsync(int serviceId);
}

public interface IAppointmentService
{
    Task<List<AppointmentDto>> GetAppointmentsAsync();
    Task<List<AppointmentDto>> GetAppointmentsByPatientAsync(int patientId);
    Task<List<AppointmentDto>> GetAppointmentsByProviderAsync(int providerId);
    Task<List<AppointmentDto>> GetAppointmentsByDateAsync(string date);
    Task<AppointmentDto?> GetAppointmentAsync(int appointmentId);
    Task<AppointmentDto> CreateAppointmentAsync(CreateAppointmentDto appointment);
    Task<AppointmentDto> UpdateAppointmentAsync(int appointmentId, AppointmentDto appointment);
    Task UpdateStatusAsync(int appointmentId, string status);
    Task CancelAppointmentAsync(int appointmentId, string? reason);
}

public interface IPatientService
{
    Task<List<PatientDto>> GetPatientsAsync();
    Task<PatientDto?> GetPatientAsync(int patientId);
    Task<PatientDto> CreatePatientAsync(PatientDto patient);
    Task<PatientDto> UpdatePatientAsync(int patientId, PatientDto patient);
}

public interface IProviderService
{
    Task<List<ProviderDto>> GetProvidersAsync();
    Task<ProviderDto?> GetProviderAsync(int providerId);
    Task<List<ProviderDto>> GetProvidersByServiceAsync(int serviceId);
}

public interface IInvoiceService
{
    Task<List<InvoiceDto>> GetInvoicesAsync();
    Task<List<InvoiceDto>> GetInvoicesByPatientAsync(int patientId);
    Task<InvoiceDto?> GetInvoiceAsync(int invoiceId);
    Task<List<InvoiceLineItemDto>> GetInvoiceLineItemsAsync(int invoiceId);
    Task<InvoiceDto> CreateInvoiceAsync(InvoiceDto invoice);
    Task<InvoiceDto> UpdateInvoiceAsync(int invoiceId, InvoiceDto invoice);
}

public interface IPaymentService
{
    Task<List<PaymentDto>> GetPaymentsByInvoiceAsync(int invoiceId);
    Task<List<PaymentDto>> GetPaymentsByPatientAsync(int patientId);
    Task<PaymentDto> CreatePaymentAsync(CreatePaymentDto payment);
}

public interface IMedicalRecordService
{
    Task<MedicalRecordDto?> GetRecordByPatientAsync(int patientId);
    Task<MedicalRecordDto> UpsertRecordAsync(MedicalRecordDto record);
    Task<List<VisitNoteDto>> GetVisitNotesByPatientAsync(int patientId);
    Task<VisitNoteDto> CreateVisitNoteAsync(VisitNoteDto visitNote);
}

public interface IStaffService
{
    Task<List<StaffDto>> GetStaffAsync();
    Task<StaffDto?> GetStaffMemberAsync(int staffId);
    Task<StaffDto> CreateStaffAsync(StaffDto staff);
    Task<StaffDto> UpdateStaffAsync(int staffId, StaffDto staff);
    Task<List<ShiftDto>> GetShiftsAsync();
    Task<List<ShiftDto>> GetShiftsByStaffAsync(int staffId);
    Task<ShiftDto> CreateShiftAsync(ShiftDto shift);
    Task<ShiftDto> UpdateShiftAsync(int shiftId, ShiftDto shift);
    Task DeleteShiftAsync(int shiftId);
}

public interface IInsuranceService
{
    Task<List<InsuranceProviderDto>> GetInsuranceProvidersAsync();
    Task<InsuranceProviderDto> CreateInsuranceProviderAsync(InsuranceProviderDto provider);
    Task<InsuranceProviderDto> UpdateInsuranceProviderAsync(int id, InsuranceProviderDto provider);
    Task<List<PatientInsuranceDto>> GetPatientInsuranceAsync(int patientId);
    Task<List<InsuranceClaimDto>> GetClaimsAsync();
    Task<InsuranceClaimDto> CreateClaimAsync(InsuranceClaimDto claim);
    Task<InsuranceClaimDto> UpdateClaimAsync(int claimId, InsuranceClaimDto claim);
}

public interface IAuthService
{
    Task<UserAccountDto?> GetUserByEmailAsync(string email);
    Task<UserAccountDto> CreateUserAsync(CreateUserAccountDto user);
    Task<bool> ValidatePasswordAsync(string inputPassword, string storedHash);
    string HashPassword(string password);
}
