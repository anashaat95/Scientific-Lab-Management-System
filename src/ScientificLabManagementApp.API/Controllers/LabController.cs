namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class LabController :
    ControllerBaseWithEndpoints<
        LabDto, GetOneLabByIdQuery, GetManyLabQuery,
        LabCommandData, AddLabCommandData,  UpdateLabCommandData,
        AddLabCommand, UpdateLabCommand, DeleteLabCommand>

{
    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyLabSelectOptionsQuery());
        return Result.Create(response);
    }
}