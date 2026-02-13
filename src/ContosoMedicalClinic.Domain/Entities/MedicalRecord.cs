namespace ContosoMedicalClinic.Domain.Entities;

public class MedicalRecord
{
    public int RecordId { get; set; }
    public int PatientId { get; set; }
    public string? BloodType { get; set; }
    public string? Allergies { get; set; }
    public string? CurrentMedications { get; set; }
    public string? ChronicConditions { get; set; }
    public string? FamilyHistory { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public string? PatientName { get; set; }
}

public class VisitNote
{
    public int VisitNoteId { get; set; }
    public int AppointmentId { get; set; }
    public int PatientId { get; set; }
    public int ProviderId { get; set; }
    public string? ChiefComplaint { get; set; }
    public string? Diagnosis { get; set; }
    public string? TreatmentPlan { get; set; }
    public string? Prescriptions { get; set; }
    public string? FollowUpNotes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public string? PatientName { get; set; }
    public string? ProviderName { get; set; }
    public DateTime? AppointmentDate { get; set; }
}
