namespace ScientificLabManagementApp.Domain;

public class ApplicationRole : IdentityRole<string>, IEntityBase
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
