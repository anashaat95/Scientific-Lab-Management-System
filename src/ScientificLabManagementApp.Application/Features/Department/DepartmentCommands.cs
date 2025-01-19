namespace ScientificLabManagementApp.Application;

public class DepartmentCommandData
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string company_id { get; set; }
}

public class AddDepartmentCommand : AddCommandBase<DepartmentDto, DepartmentCommandData>{}

public class UpdateDepartmentCommand : UpdateCommandBase<DepartmentDto, DepartmentCommandData>{}

public class DeleteDepartmentCommand : DeleteCommandBase<DepartmentDto>{}

