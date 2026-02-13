namespace ContosoMedicalClinic.Application.DTOs;

public record DabResponse<T>(T[] Value);

public record AppointmentDto(
    int AppointmentId, int PatientId, int ProviderId, int ServiceId,
    string AppointmentDate, string StartTime, string EndTime,
    string Status, string? Notes, string? CancellationReason);

public record CreateAppointmentDto(
    int PatientId, int ProviderId, int ServiceId,
    string AppointmentDate, string StartTime, string EndTime,
    string Status = "Scheduled", string? Notes = null);

public record ServiceDto(
    int ServiceId, int CategoryId, string Name, string? Description,
    int DefaultDurationMinutes, decimal DefaultPrice, string? ProcedureCode, bool IsActive);

public record ServiceCategoryDto(
    int CategoryId, string Name, string? Description,
    string? IconCssClass, int SortOrder, bool IsActive);

public record PatientDto(
    int PatientId, string FirstName, string LastName, string DateOfBirth,
    string? Gender, string Email, string? Phone, string? Address,
    string? City, string? State, string? ZipCode,
    string? EmergencyContactName, string? EmergencyContactPhone, bool IsActive);

public record ProviderDto(
    int ProviderId, int StaffId, string? Title, string? Bio,
    string? PhotoUrl, bool AcceptsNewPatients, bool IsActive,
    string? FirstName = null, string? LastName = null,
    string? Email = null, string? Phone = null,
    string? Specialization = null, string? LicenseNumber = null);

public record StaffDto(
    int StaffId, string FirstName, string LastName, string Email,
    string? Phone, string Role, string? Specialization,
    string? LicenseNumber, string HireDate, bool IsActive);

public record ShiftDto(
    int ShiftId, int StaffId, string ShiftDate,
    string StartTime, string EndTime, string? ShiftType, string? Notes);

public record InvoiceDto(
    int InvoiceId, int PatientId, int? AppointmentId, string InvoiceNumber,
    string InvoiceDate, string DueDate, decimal SubTotal,
    decimal InsuranceAdjustment, decimal TotalAmount, decimal BalanceDue, string Status);

public record InvoiceLineItemDto(
    int LineItemId, int InvoiceId, int ServiceId,
    string Description, int Quantity, decimal UnitPrice, decimal Amount);

public record PaymentDto(
    int PaymentId, int InvoiceId, int PatientId, string PaymentDate,
    decimal Amount, string PaymentMethod, string? ReferenceNumber, string? Notes);

public record CreatePaymentDto(
    int InvoiceId, int PatientId, decimal Amount,
    string PaymentMethod, string? ReferenceNumber = null, string? Notes = null);

public record MedicalRecordDto(
    int RecordId, int PatientId, string? BloodType, string? Allergies,
    string? CurrentMedications, string? ChronicConditions, string? FamilyHistory);

public record VisitNoteDto(
    int VisitNoteId, int AppointmentId, int PatientId, int ProviderId,
    string? ChiefComplaint, string? Diagnosis, string? TreatmentPlan,
    string? Prescriptions, string? FollowUpNotes);

public record InsuranceProviderDto(
    int InsuranceProviderId, string Name, string? ContactPhone,
    string? ContactEmail, string? Website, bool IsActive);

public record InsuranceClaimDto(
    int ClaimId, int PatientInsuranceId, int InvoiceId, string? ClaimNumber,
    string SubmissionDate, decimal ClaimedAmount, decimal? ApprovedAmount,
    string Status, string? DenialReason, string? ProcessedDate);

public record PatientInsuranceDto(
    int PatientInsuranceId, int PatientId, int InsuranceProviderId,
    string PolicyNumber, string? GroupNumber, string? SubscriberName,
    string EffectiveDate, string? ExpirationDate, bool IsPrimary, bool IsActive);

public record UserAccountDto(
    int UserAccountId, string Email, string PasswordHash, string DisplayName,
    string Role, int? PatientId, int? StaffId, bool IsActive);

public record CreateUserAccountDto(
    string Email, string PasswordHash, string DisplayName, string Role,
    int? PatientId = null, int? StaffId = null);

public record LoginDto(string Email, string Password);

public record RegisterDto(string Email, string Password, string DisplayName, string Role);
