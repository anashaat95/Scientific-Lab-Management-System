namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController :
    ControllerBaseWithEndpoints<
        EquipmentDto, GetOneEquipmentByIdQuery, GetManyEquipmentQuery,
        EquipmentCommandData, AddEquipmentCommandData, UpdateEquipmentCommandData,
        AddEquipmentCommand, UpdateEquipmentCommand, DeleteEquipmentCommand>

{

    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyEquipmentSelectOptionsQuery());
        return Result.Create(response);
    }
}