namespace ScientificLabManagementApp.Application;

public class MaintenanceLogCommandData
{
    public required string Description { get; set; }
    public required enMaintenanceStatus Status { get; set; } = enMaintenanceStatus.InProgress;
    public required string equipment_id { get; set; }
    public required string technician_id { get; set; }
}
public class AddMaintenanceLogCommand : AddCommandBase<MaintenanceLogDto, MaintenanceLogCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.TechnicianLevel;
}

public class UpdateMaintenanceLogCommand : UpdateCommandBase<MaintenanceLogDto, MaintenanceLogCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.TechnicianLevel;
}

public class DeleteMaintenanceLogCommand : DeleteCommandBase<MaintenanceLogDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.TechnicianLevel;
}