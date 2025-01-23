namespace ScientificLabManagementApp.Application;

public class DepartmentCommandData
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string company_id { get; set; }
}

public class AddDepartmentCommand : AddCommandBase<DepartmentDto, DepartmentCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateDepartmentCommand : UpdateCommandBase<DepartmentDto, DepartmentCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteDepartmentCommand : DeleteCommandBase<DepartmentDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

