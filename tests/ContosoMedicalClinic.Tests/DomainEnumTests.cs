using ContosoMedicalClinic.Domain.Enums;

namespace ContosoMedicalClinic.Tests;

public class DomainEnumTests
{
    [Theory]
    [InlineData("Scheduled", AppointmentStatus.Scheduled)]
    [InlineData("Confirmed", AppointmentStatus.Confirmed)]
    [InlineData("InProgress", AppointmentStatus.InProgress)]
    [InlineData("Completed", AppointmentStatus.Completed)]
    [InlineData("Cancelled", AppointmentStatus.Cancelled)]
    [InlineData("NoShow", AppointmentStatus.NoShow)]
    public void AppointmentStatus_ParsesFromString(string input, AppointmentStatus expected)
    {
        var parsed = Enum.Parse<AppointmentStatus>(input);
        Assert.Equal(expected, parsed);
    }

    [Theory]
    [InlineData("Draft", InvoiceStatus.Draft)]
    [InlineData("Sent", InvoiceStatus.Sent)]
    [InlineData("Paid", InvoiceStatus.Paid)]
    [InlineData("PartiallyPaid", InvoiceStatus.PartiallyPaid)]
    [InlineData("Overdue", InvoiceStatus.Overdue)]
    [InlineData("Cancelled", InvoiceStatus.Cancelled)]
    public void InvoiceStatus_ParsesFromString(string input, InvoiceStatus expected)
    {
        var parsed = Enum.Parse<InvoiceStatus>(input);
        Assert.Equal(expected, parsed);
    }

    [Theory]
    [InlineData("Cash", PaymentMethod.Cash)]
    [InlineData("CreditCard", PaymentMethod.CreditCard)]
    [InlineData("DebitCard", PaymentMethod.DebitCard)]
    [InlineData("Insurance", PaymentMethod.Insurance)]
    [InlineData("Check", PaymentMethod.Check)]
    public void PaymentMethod_ParsesFromString(string input, PaymentMethod expected)
    {
        var parsed = Enum.Parse<PaymentMethod>(input);
        Assert.Equal(expected, parsed);
    }

    [Theory]
    [InlineData("Doctor", StaffRole.Doctor)]
    [InlineData("Nurse", StaffRole.Nurse)]
    [InlineData("FrontOffice", StaffRole.FrontOffice)]
    [InlineData("Admin", StaffRole.Admin)]
    public void StaffRole_ParsesFromString(string input, StaffRole expected)
    {
        var parsed = Enum.Parse<StaffRole>(input);
        Assert.Equal(expected, parsed);
    }

    [Theory]
    [InlineData("Morning", ShiftType.Morning)]
    [InlineData("Afternoon", ShiftType.Afternoon)]
    [InlineData("Evening", ShiftType.Evening)]
    [InlineData("Night", ShiftType.Night)]
    public void ShiftType_ParsesFromString(string input, ShiftType expected)
    {
        var parsed = Enum.Parse<ShiftType>(input);
        Assert.Equal(expected, parsed);
    }

    [Theory]
    [InlineData("Submitted", ClaimStatus.Submitted)]
    [InlineData("UnderReview", ClaimStatus.UnderReview)]
    [InlineData("Approved", ClaimStatus.Approved)]
    [InlineData("Denied", ClaimStatus.Denied)]
    public void ClaimStatus_ParsesFromString(string input, ClaimStatus expected)
    {
        var parsed = Enum.Parse<ClaimStatus>(input);
        Assert.Equal(expected, parsed);
    }

    [Fact]
    public void AppointmentStatus_HasExpectedCount()
    {
        Assert.Equal(6, Enum.GetValues<AppointmentStatus>().Length);
    }

    [Fact]
    public void InvoiceStatus_HasExpectedCount()
    {
        Assert.Equal(6, Enum.GetValues<InvoiceStatus>().Length);
    }

    [Fact]
    public void PaymentMethod_HasExpectedCount()
    {
        Assert.Equal(5, Enum.GetValues<PaymentMethod>().Length);
    }
}
