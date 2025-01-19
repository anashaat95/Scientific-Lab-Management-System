namespace ScientificLabManagementApp.Domain;

public class City : EntityBase
{
    public string Name { get; set; }
    public ICollection<Company>? Companies { get; set; }
}
