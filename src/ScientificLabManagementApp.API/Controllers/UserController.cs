namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class UserController :
    ControllerBaseWithEndpoints<
        UserDto, GetOneUserByIdQuery, GetManyUserQuery,
        UserCommandData, AddUserCommandData, UpdateUserCommandData,
        AddUserCommand, UpdateUserCommand, DeleteUserCommand>

{

}