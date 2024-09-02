namespace RetailPortal.Models;

public class ProductDetails
{
    public int ProductID { get; set; } // Primary Key
    public string? ProductName { get; set; }
    public string? ProductType { get; set; }
    public decimal CoverageAmount { get; set; }
    public decimal PremiumAmount { get; set; }
    public DateTime? EffectiveDate { get; set; }
    public DateTime? ExpiryDate { get; set; }

    // Foreign Key
    public int PolicyNumber { get; set; }
    public PolicyDetails? PolicyDetails { get; set; }

    // Foreign Key
    public int MemberId { get; set; }
    public MemberDetails? MemberDetails { get; set; }
}
