namespace ScientificLabManagementApp.Application;

public class RoleCommandData
{
    public required string Name { get; set; }
}

public class AddRoleCommand : AddCommandBase<RoleDto, RoleCommandData>{}

public class UpdateRoleCommand : UpdateCommandBase<RoleDto, RoleCommandData>{}

public class DeleteRoleCommand : DeleteCommandBase<RoleDto>{}