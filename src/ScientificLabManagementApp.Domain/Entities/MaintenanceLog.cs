namespace ScientificLabManagementApp.Domain;

public class MaintenanceLog : EntityBase
{
    public string Description { get; set; }
    public enMaintenanceStatus Status { get; set; }
    public string EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;
    public string TechnicianId { get; set; }
    public virtual ApplicationUser Technician { get; set; } = null!;
}