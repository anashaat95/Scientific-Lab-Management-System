namespace ScientificLabManagementApp.Application;
public class LabDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string opening_time { get; set; }
    public string closing_time { get; set; }
    public string supervisor_url { get; set; }
    public string supervisor_name { get; set; }
    public string department_name { get; set; }
    public string department_url { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}
