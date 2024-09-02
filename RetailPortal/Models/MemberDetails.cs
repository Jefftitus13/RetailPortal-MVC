namespace RetailPortal.Models;

public class MemberDetails
{
    public int MemberId { get; set; } // Primary Key
    public int PolicyNumber { get; set; } // Foreign Key
    public string? MemberName { get; set; }
    public DateTime? MemberDOB { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Gender { get; set; } // "Male" or "Female"
    public string? MaritalStatus { get; set; }
    public string? State { get; set; }
    public string? District { get; set; }
    public string? CurrentSalary { get; set; }
    public decimal Height { get; set; } // cm
    public decimal Weight { get; set; } // kg
    public string? RelationshipToPolicyholder { get; set; }

    // Navigation property for PolicyDetails
    public PolicyDetails? PolicyDetails { get; set; }

    // Navigation property for related ProductDetails
    public ICollection<ProductDetails>? ProductDetails { get; set; }

    // Navigation property for related Payments
    public ICollection<Payment>? Payments { get; set; }
}
