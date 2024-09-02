namespace RetailPortal.Models;

public class Payment
{
    public int PaymentID { get; set; } // Primary Key
    public string? CardNumber { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public string? CVV { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }

    // Foreign Key
    public int PolicyNumber { get; set; }
    public PolicyDetails? PolicyDetails { get; set; }

    // Foreign Key
    public int MemberId { get; set; }
    public MemberDetails? MemberDetails { get; set; }
}
