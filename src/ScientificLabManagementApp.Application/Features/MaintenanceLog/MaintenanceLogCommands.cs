namespace ScientificLabManagementApp.Application;

public abstract class MaintenanceLogCommandData
{
    public required string Description { get; set; }
    public required enMaintenanceStatus Status { get; set; } = enMaintenanceStatus.InMaintenance;
    public required string equipment_id { get; set; }
    public required string technician_id { get; set; }
}

public class AddMaintenanceLogCommandData : MaintenanceLogCommandData { }
public class UpdateMaintenanceLogCommandData : MaintenanceLogCommandData { }

public class AddMaintenanceLogCommand : AddCommandBase<MaintenanceLogDto, AddMaintenanceLogCommandData>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.TechnicianLevel;
}

public class UpdateMaintenanceLogCommand : UpdateCommandBase<MaintenanceLogDto, UpdateMaintenanceLogCommandData>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.TechnicianLevel;
}

public class DeleteMaintenanceLogCommand : DeleteCommandBase<MaintenanceLogDto>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.TechnicianLevel;
}