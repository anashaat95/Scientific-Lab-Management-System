namespace ScientificLabManagementApp.Domain;

public class Country : EntityBase
{
    public string Name { get; set; }
    public ICollection<Company>? Companies { get; set; }
}
