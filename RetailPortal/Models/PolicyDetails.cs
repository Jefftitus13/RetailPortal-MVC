namespace RetailPortal.Models;

public class PolicyDetails
{
    public int? PolicyNumber { get; set; } // Primary Key
    public string? PolicyType { get; set; }
    public decimal? CoverageAmount { get; set; }
    public decimal? PremiumAmount { get; set; }
    public string? PolicyStatus { get; set; }
    public string? PolicyholderName { get; set; }
    public string? ContactInformation { get; set; }
    public string? InsuredName { get; set; }
    public DateTime? LastPaymentDate { get; set; }
    public DateTime? NextDueDate { get; set; }
    public string? PaymentStatus { get; set; }

    // Foreign Key
    public int SponsorId { get; set; }
    public SponsorDetails? SponsorDetails { get; set; }

    // Navigation properties for related MemberDetails, ProductDetails, and Payments
    public ICollection<MemberDetails>? MemberDetails { get; set; }
    public ICollection<ProductDetails>? ProductDetails { get; set; }
    public ICollection<Payment>? Payments { get; set; }
}
