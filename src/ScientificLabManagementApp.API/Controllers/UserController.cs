using CloudinaryDotNet.Actions;

namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class UserController :
    ControllerBaseWithEndpoints<
        UserDto, GetOneUserByIdQuery, GetManyUserQuery,
        UserCommandData, AddUserCommandData, UpdateUserCommandData,
        AddUserCommand, UpdateUserCommand, DeleteUserCommand>
{
    // GET api/<Controller>/5
    [HttpGet("field/{Field}")]
    public virtual async Task<ActionResult<UserDto>> GetByField(GetOneUserByFieldQueryBase command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

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
        var response = await Mediator.Send(new GetManyLabSupervisorQuery());
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
        var response = await Mediator.Send(new GetManyResearcherQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/researcher/options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllResearchersOptions()
    {
        var response = await Mediator.Send(new GetManyResearcherSelectOptionsQuery());
        return Result.Create(response);
    }

    // GET api/<Controller>/email
    [HttpGet("query")]
    public virtual async Task<ActionResult<UserDto>> GetByEmail(GetOneUserByEmailQuery command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    // GET api/<Controller>/email
    [HttpGet("exist")]
    public virtual async Task<ActionResult<bool>> ExistByEmail(ExistOneUserByEmailQuery command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

}