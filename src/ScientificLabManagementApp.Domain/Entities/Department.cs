namespace ScientificLabManagementApp.Domain;

public class Department : EntityBase
{
    public string Name { get; set; }
    public string Location { get; set; }

    public string CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;
    public virtual ICollection<Lab>? Labs { get; set; } = null!;

    public virtual ICollection<ApplicationUser> Users { get; set; } = null!;
}
