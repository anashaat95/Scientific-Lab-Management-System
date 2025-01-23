namespace ScientificLabManagementApp.Application;
public class DepartmentDto : IEntityHaveId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Location { get; set; }
    public string company_url { get; set; }
    public string company_name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}
