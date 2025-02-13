namespace ScientificLabManagementApp.Application;

public abstract class DepartmentCommandData
{
    public required string Name { get; set; }
    public required string Location { get; set; }
    public required string company_id { get; set; }
}

public class AddDepartmentCommandData : DepartmentCommandData { }
public class UpdateDepartmentCommandData : DepartmentCommandData { }

public class AddDepartmentCommand : AddCommandBase<DepartmentDto, AddDepartmentCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateDepartmentCommand : UpdateCommandBase<DepartmentDto, UpdateDepartmentCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteDepartmentCommand : DeleteCommandBase<DepartmentDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

