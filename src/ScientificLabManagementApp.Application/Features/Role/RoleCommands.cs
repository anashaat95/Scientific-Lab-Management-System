namespace ScientificLabManagementApp.Application;

public abstract class RoleCommandData
{
    public required string Name { get; set; }
}

public class AddRoleCommandData : RoleCommandData {}

public class UpdateRoleCommandData : RoleCommandData { }

public class AddRoleCommand : AddCommandBase<RoleDto, AddRoleCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class UpdateRoleCommand : UpdateCommandBase<RoleDto, UpdateRoleCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class DeleteRoleCommand : DeleteCommandBase<RoleDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}