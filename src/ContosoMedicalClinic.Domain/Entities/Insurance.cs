using ContosoMedicalClinic.Domain.Enums;

namespace ContosoMedicalClinic.Domain.Entities;

public class InsuranceProvider
{
    public int InsuranceProviderId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ContactPhone { get; set; }
    public string? ContactEmail { get; set; }
    public string? Website { get; set; }
    public bool IsActive { get; set; } = true;
}

public class PatientInsurance
{
    public int PatientInsuranceId { get; set; }
    public int PatientId { get; set; }
    public int InsuranceProviderId { get; set; }
    public string PolicyNumber { get; set; } = string.Empty;
    public string? GroupNumber { get; set; }
    public string? SubscriberName { get; set; }
    public DateTime EffectiveDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public bool IsPrimary { get; set; } = true;
    public bool IsActive { get; set; } = true;

    // Navigation
    public string? PatientName { get; set; }
    public string? InsuranceProviderName { get; set; }
}

public class InsuranceClaim
{
    public int ClaimId { get; set; }
    public int PatientInsuranceId { get; set; }
    public int InvoiceId { get; set; }
    public string? ClaimNumber { get; set; }
    public DateTime SubmissionDate { get; set; }
    public decimal ClaimedAmount { get; set; }
    public decimal? ApprovedAmount { get; set; }
    public ClaimStatus Status { get; set; }
    public string? DenialReason { get; set; }
    public DateTime? ProcessedDate { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public string? PatientName { get; set; }
    public string? InvoiceNumber { get; set; }
    public string? InsuranceProviderName { get; set; }
}
