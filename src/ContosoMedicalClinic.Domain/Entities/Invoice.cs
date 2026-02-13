using ContosoMedicalClinic.Domain.Enums;

namespace ContosoMedicalClinic.Domain.Entities;

public class Invoice
{
    public int InvoiceId { get; set; }
    public int PatientId { get; set; }
    public int? AppointmentId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal SubTotal { get; set; }
    public decimal InsuranceAdjustment { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal BalanceDue { get; set; }
    public InvoiceStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public string? PatientName { get; set; }
    public List<InvoiceLineItem> LineItems { get; set; } = [];
}

public class InvoiceLineItem
{
    public int LineItemId { get; set; }
    public int InvoiceId { get; set; }
    public int ServiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal Amount { get; set; }

    // Navigation
    public string? ServiceName { get; set; }
}

public class Payment
{
    public int PaymentId { get; set; }
    public int InvoiceId { get; set; }
    public int PatientId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string? ReferenceNumber { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation
    public string? PatientName { get; set; }
    public string? InvoiceNumber { get; set; }
}
