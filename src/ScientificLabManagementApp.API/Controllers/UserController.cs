namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class UserController :
    ControllerBaseWithEndpoints<
        UserDto, GetOneUserByIdQuery, GetManyUserQuery,
        UserCommandData, AddUserCommandData, UpdateUserCommandData,
        AddUserCommand, UpdateUserCommand, DeleteUserCommand>
{
    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllUsersOptions()
    {
        var response = await Mediator.Send(new GetManyUserSelectOptionsQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/technician")]
    public virtual async Task<ActionResult<IEnumerable<UserDto>>> GetTechnicians()
    {
        var response = await Mediator.Send(new GetManyTechnicianQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/technician/options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllTechniciansOptions()
    {
        var response = await Mediator.Send(new GetManyTechnicianSelectOptionsQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/lab-supervisor")]
    public virtual async Task<ActionResult<IEnumerable<UserDto>>> GetLabSupervisors()
    {
        var response = await Mediator.Send(new GetManySupervisorQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/lab-supervisor/options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllLabSupervisorsOptions()
    {
        var response = await Mediator.Send(new GetManySupervisorSelectOptionsQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/researcher")]
    public virtual async Task<ActionResult<IEnumerable<UserDto>>> GetResearchers()
    {
        var response = await Mediator.Send(new GetManySupervisorQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/researcher/options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllResearchersOptions()
    {
        var response = await Mediator.Send(new GetManyResearcherSelectOptionsQuery());
        return Result.Create(response);
    }
}