namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class RoleController :
    ControllerBaseWithEndpoints<
        RoleDto, GetOneRoleByIdQuery, GetManyRoleQuery,
        RoleCommandData, AddRoleCommandData,  UpdateRoleCommandData, 
        AddRoleCommand, UpdateRoleCommand, DeleteRoleCommand>

{
}