using ContosoMedicalClinic.Domain.Enums;

namespace ContosoMedicalClinic.Domain.Entities;

public class Staff
{
    public int StaffId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public StaffRole Role { get; set; }
    public string? Specialization { get; set; }
    public string? LicenseNumber { get; set; }
    public DateTime HireDate { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; }

    public string FullName => $"{FirstName} {LastName}";
}

public class Provider
{
    public int ProviderId { get; set; }
    public int StaffId { get; set; }
    public string? Title { get; set; }
    public string? Bio { get; set; }
    public string? PhotoUrl { get; set; }
    public bool AcceptsNewPatients { get; set; } = true;
    public bool IsActive { get; set; } = true;

    // Navigation helpers (populated from joined queries)
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Specialization { get; set; }

    public string DisplayName => $"{Title} {FirstName} {LastName}".Trim();
}

public class Shift
{
    public int ShiftId { get; set; }
    public int StaffId { get; set; }
    public DateTime ShiftDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public ShiftType ShiftType { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public string? StaffName { get; set; }
}
