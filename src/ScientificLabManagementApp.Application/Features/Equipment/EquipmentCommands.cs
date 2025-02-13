namespace ScientificLabManagementApp.Application;

public abstract class EquipmentCommandData
{
    public string Name { get; set; }
    public int total_quantity { get; set; }
    public enEquipmentType Type { get; set; }
    public enEquipmentStatus Status { get; set; } = enEquipmentStatus.Available;
    public DateTime purchase_date { get; set; }
    public string? serial_number { get; set; }
    public string? Specifications { get; set; }
    public string? Description { get; set; }
    public bool can_be_Left_overnight { get; set; } = false;
    public string? image_url { get; set; }
    public string parent_equipment_id { get; set; }
    public string company_id { get; set; }
}


public class AddEquipmentCommandData : EquipmentCommandData { }
public class UpdateEquipmentCommandData : EquipmentCommandData { }

public class AddEquipmentCommand : AddCommandBase<EquipmentDto, AddEquipmentCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateEquipmentCommand : UpdateCommandBase<EquipmentDto, UpdateEquipmentCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteEquipmentCommand : DeleteCommandBase<EquipmentDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}