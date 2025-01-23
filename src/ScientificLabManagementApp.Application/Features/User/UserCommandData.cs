namespace ScientificLabManagementApp.Application;

public class UserCommandData
{
    public string UserName { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string Email { get; set; }
    public string? phone_number { get; set; }
    public string? image_url { get; set; }
    public string company_id { get; set; }
    public string department_id { get; set; }
    public string lab_id { get; set; }
    public string? google_scholar_url { get; set; }
    public string? academia_url { get; set; }
    public string? scopus_url { get; set; }
    public string? researcher_gate_url { get; set; }
    public string? expertise_area { get; set; }
}

public class AddUserCommandData : UserCommandData
{
    public string Password { get; set; }
    public string confirm_password { get; set; }
}

public class AddUserCommand : AddCommandBase<UserDto, UserCommandData>
{
    public new AddUserCommandData Data { get; set; }

    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class UpdateUserCommand : UpdateCommandBase<UserDto, UserCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class DeleteUserCommand : DeleteCommandBase<UserDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}