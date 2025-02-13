namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController :
    ControllerBaseWithEndpoints<
        EquipmentDto, GetOneEquipmentByIdQuery, GetManyEquipmentQuery,
        EquipmentCommandData, AddEquipmentCommandData, UpdateEquipmentCommandData,
        AddEquipmentCommand, UpdateEquipmentCommand, DeleteEquipmentCommand>

{
}