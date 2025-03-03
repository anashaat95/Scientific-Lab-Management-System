
namespace ScientificLabManagementApp.Application;
public class GetManyDepartmentHandler : GetManyQueryHandlerBase<GetManyDepartmentQuery, Department, DepartmentDto>
{
    protected override Task<PagedList<DepartmentDto>> GetEntityDtos(GetManyDepartmentQuery request)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        return _basicService.GetAllAsync(parameters, e => e.Company);
    }
}
public class GetManyDepartmentSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyDepartmentSelectOptionsQuery, Department> { }
public class GetOneDepartmentByIdHandler : GetOneQueryHandlerBase<GetOneDepartmentByIdQuery, Department, DepartmentDto>
{
    protected override Task<DepartmentDto?> GetEntityDto(GetOneDepartmentByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Company);
    }
}

public class AddDepartmentHandler : AddCommandHandlerBase<AddDepartmentCommand, Department, DepartmentDto> { }

public class DeleteDepartmentHandler : DeleteCommandHandlerBase<DeleteDepartmentCommand, Department, DepartmentDto> { }

public class UpdateDepartmentHandler : UpdateCommandHandlerBase<UpdateDepartmentCommand, Department, DepartmentDto> { }
