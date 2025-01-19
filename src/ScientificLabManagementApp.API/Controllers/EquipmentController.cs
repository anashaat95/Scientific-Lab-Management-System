namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController :
    ControllerBaseWithEndpoints<
        EquipmentDto, GetOneEquipmentByIdQuery, GetManyEquipmentQuery,
        EquipmentCommandData, AddEquipmentCommand, UpdateEquipmentCommand, DeleteEquipmentCommand>

{
}