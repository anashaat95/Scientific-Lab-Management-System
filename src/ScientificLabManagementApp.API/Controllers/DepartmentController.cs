namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController :
    ControllerBaseWithEndpoints<
        DepartmentDto, GetOneDepartmentByIdQuery, GetManyDepartmentQuery,
        DepartmentCommandData, AddDepartmentCommandData, UpdateDepartmentCommandData,
        AddDepartmentCommand, UpdateDepartmentCommand, DeleteDepartmentCommand>

{
}