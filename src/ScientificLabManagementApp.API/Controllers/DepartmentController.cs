namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController :
    ControllerBaseWithEndpoints<
        DepartmentDto, GetOneDepartmentByIdQuery, GetManyDepartmentQuery,
        DepartmentCommandData, AddDepartmentCommand, UpdateDepartmentCommand, DeleteDepartmentCommand>

{
}