namespace ScientificLabManagementApp.Application;
public class GetManyDepartmentHandler : GetManyQueryHandlerBase<GetManyDepartmentQuery, Department, DepartmentDto> { }

public class GetOneDepartmentByIdHandler : GetOneQueryHandlerBase<GetOneDepartmentByIdQuery, Department, DepartmentDto> { }

public class AddDepartmentHandler : AddCommandHandlerBase<AddDepartmentCommand, Department, DepartmentDto> { }

public class DeleteDepartmentHandler : DeleteCommandHandlerBase<DeleteDepartmentCommand, Department, DepartmentDto> { }

public class UpdateDepartmentHandler : UpdateCommandHandlerBase<UpdateDepartmentCommand, Department, DepartmentDto> { }
