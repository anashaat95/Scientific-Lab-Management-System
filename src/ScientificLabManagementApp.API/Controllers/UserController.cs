namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class UserController :
    ControllerBaseWithEndpoints<
        UserDto, GetOneUserByIdQuery, GetManyUserQuery,
        UserCommandData, AddUserCommandData, UpdateUserCommandData,
        AddUserCommand, UpdateUserCommand, DeleteUserCommand>
{
    [HttpGet("/api/technicians")]
    public virtual async Task<ActionResult<IEnumerable<UserDto>>> GetTechnicians()
    {
        var response = await Mediator.Send(new GetManyTechnicianQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/lab-supervisors")]
    public virtual async Task<ActionResult<IEnumerable<UserDto>>> GetLabSupervisors()
    {
        var response = await Mediator.Send(new GetManySupervisiorQuery());
        return Result.Create(response);
    }

    [HttpGet("/api/researchers")]
    public virtual async Task<ActionResult<IEnumerable<UserDto>>> GetResearchers()
    {
        var response = await Mediator.Send(new GetManySupervisiorQuery());
        return Result.Create(response);
    }
}