namespace ScientificLabManagementApp.Domain;

public class ApplicationUser : IdentityUser<string>, IEntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; } = null;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string CompanyId { get; set; }
    public Company Company { get; set; } = null!;

    public string DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public string? LabId { get; set; }
    public Lab? Lab { get; set; }
    public ICollection<Booking>? Bookings { get; set; }
    public ICollection<MaintenanceLog>? MaintenanceLogs { get; set; }


    // Role-specific properties as nullable
    public string? GoogleScholarUrl { get; set; }
    public string? AcademiaUrl { get; set; }
    public string? ScopusUrl { get; set; }
    public string? ResearcherGateUrl { get; set; }
    public string? ExpertiseArea { get; set; }
}
