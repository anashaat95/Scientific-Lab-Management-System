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

    [HttpGet("query/{Name}")]
    public virtual async Task<ActionResult<LabDto>> Get(GetOneLabByNameQuery command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }
}