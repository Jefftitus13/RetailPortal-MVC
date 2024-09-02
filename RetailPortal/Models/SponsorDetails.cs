namespace RetailPortal.Models;

public class SponsorDetails
{
    public int SponsorId { get; set; } // Primary Key
    public string? SponsorName { get; set; }
    public string? SponsorEmail { get; set; }
    public int PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; } // "Male" or "Female"

    // Navigation property for related PolicyDetails
    public ICollection<PolicyDetails>? PolicyDetails { get; set; }
}
