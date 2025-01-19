namespace ScientificLabManagementApp.Domain;

public class Company : EntityBase
{
    public string Name { get; set; }
    public string? Street { get; set; }
    public string? ZipCode { get; set; }
    public string? Website { get; set; }

    public string CityId { get; set; }
    public virtual City City { get; set; } = null!;

    public string CountryId { get; set; }
    public virtual Country Country { get; set; } = null!;

    public ICollection<Department> Departments { get; set; } = null!;
    public ICollection<Equipment>? Equipments { get; set; } = null!;
    public ICollection<ApplicationUser> Users { get; set; } = null!;
}
