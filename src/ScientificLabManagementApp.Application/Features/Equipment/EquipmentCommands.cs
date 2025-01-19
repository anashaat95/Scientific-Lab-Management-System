namespace ScientificLabManagementApp.Application;

public class EquipmentCommandData 
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

public class AddEquipmentCommand : AddCommandBase<EquipmentDto, EquipmentCommandData>{}

public class UpdateEquipmentCommand : UpdateCommandBase<EquipmentDto, EquipmentCommandData>{}

public class DeleteEquipmentCommand : DeleteCommandBase<EquipmentDto>{}