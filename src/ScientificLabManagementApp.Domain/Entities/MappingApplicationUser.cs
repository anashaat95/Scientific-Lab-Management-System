namespace ScientificLabManagementApp.Domain;

public class MappingApplicationUser : IdentityUser<string>, IEntityBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Roles { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string CompanyId { get; set; }
    public string CompanyName { get; set; } = null!;

    public string DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;

    public string? LabId { get; set; }
    public string? LabName { get; set; }

    // Role-specific properties as nullable
    public string? GoogleScholarUrl { get; set; }
    public string? AcademiaUrl { get; set; }
    public string? ScopusUrl { get; set; }
    public string? ResearcherGateUrl { get; set; }
    public string? ExpertiseArea { get; set; }
}
