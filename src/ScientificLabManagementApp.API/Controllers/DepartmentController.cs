namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController :
    ControllerBaseWithEndpoints<
        DepartmentDto, GetOneDepartmentByIdQuery, GetManyDepartmentQuery,
        DepartmentCommandData, AddDepartmentCommandData, UpdateDepartmentCommandData,
        AddDepartmentCommand, UpdateDepartmentCommand, DeleteDepartmentCommand>

{
    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyDepartmentSelectOptionsQuery());
        return Result.Create(response);
    }
}