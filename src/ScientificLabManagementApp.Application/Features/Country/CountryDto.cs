namespace ScientificLabManagementApp.Application;
public class CountryDto : IEntityHaveId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}
