namespace ContosoMedicalClinic.Domain.Enums;

public enum AppointmentStatus
{
    Scheduled,
    Confirmed,
    InProgress,
    Completed,
    Cancelled,
    NoShow
}

public enum InvoiceStatus
{
    Draft,
    Sent,
    Paid,
    PartiallyPaid,
    Overdue,
    Cancelled
}

public enum PaymentMethod
{
    Cash,
    CreditCard,
    DebitCard,
    Insurance,
    Check
}

public enum StaffRole
{
    Doctor,
    Nurse,
    Hygienist,
    FrontOffice,
    Admin
}

public enum ShiftType
{
    Morning,
    Afternoon,
    Evening,
    Night
}

public enum ClaimStatus
{
    Submitted,
    UnderReview,
    Approved,
    Denied,
    Appealed
}

public enum UserRole
{
    Patient,
    FrontOffice,
    Doctor,
    Admin
}
