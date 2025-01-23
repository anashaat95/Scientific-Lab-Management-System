namespace ScientificLabManagementApp.Application;

public class RoleCommandData
{
    public required string Name { get; set; }
}

public class AddRoleCommand : AddCommandBase<RoleDto, RoleCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class UpdateRoleCommand : UpdateCommandBase<RoleDto, RoleCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class DeleteRoleCommand : DeleteCommandBase<RoleDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}