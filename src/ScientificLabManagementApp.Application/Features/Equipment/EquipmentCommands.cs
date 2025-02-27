namespace ScientificLabManagementApp.Application;

public abstract class EquipmentCommandData
{
    public string Name { get; set; }
    public int total_quantity { get; set; }
    public bool CanBeLeftOverNight { get; set; }
    public enEquipmentType Type { get; set; }
    public enEquipmentStatus Status { get; set; } = enEquipmentStatus.Available;
    public DateTime purchase_date { get; set; }
    public string? serial_number { get; set; }
    public string? Specifications { get; set; }
    public string? Description { get; set; }
    public IFormFile? image { get; set; }
    public string company_id { get; set; }
}


public class AddEquipmentCommandData : EquipmentCommandData { }
public class UpdateEquipmentCommandData : EquipmentCommandData { }

public class AddEquipmentCommand : AddCommandBase<EquipmentDto, AddEquipmentCommandData>
{
    [FromForm]
    public override AddEquipmentCommandData Data { get => base.Data; set => base.Data = value; }
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateEquipmentCommand : UpdateCommandBase<EquipmentDto, UpdateEquipmentCommandData>
{
    [FromForm]
    public override UpdateEquipmentCommandData Data { get => base.Data; set => base.Data = value; }
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteEquipmentCommand : DeleteCommandBase<EquipmentDto>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}