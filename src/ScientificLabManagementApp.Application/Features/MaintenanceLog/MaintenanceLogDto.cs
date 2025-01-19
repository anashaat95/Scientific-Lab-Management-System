namespace ScientificLabManagementApp.Application;
public class MaintenanceLogDto
{
    public string Id { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string equipment_url { get; set; }
    public string equipment_name { get; set; }
    public string technician_url { get; set; }
    public string technician_name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}

