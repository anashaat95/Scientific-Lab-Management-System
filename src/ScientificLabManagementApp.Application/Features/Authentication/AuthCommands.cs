namespace ScientificLabManagementApp.Application;

public class LoginCommand : IRequest<Response<LoginDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}


public class SignupCommand : IRequest<Response<string>>
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
    public string Password { get; set; }
    public string confirm_password { get; set; }
}

public class UpdateProfileCommand : IRequest<Response<string>>
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string? phone_number { get; set; }
    public string? image_url { get; set; }
    public string? google_scholar_url { get; set; }
    public string? academia_url { get; set; }
    public string? scopus_url { get; set; }
    public string? researcher_gate_url { get; set; }
    public string? expertise_area { get; set; }
}

public class UpdateUsernameCommand : IRequest<Response<string>>
{
    public string Username { get; set; }
}

public class SendUpdateEmailCommand : IRequest<Response<string>>
{
    public string new_email { get; set; }
}

public class UpdateEmailCommand : IRequest<Response<string>>
{
    public string user_id { get; set; }
    public string token { get; set; }
    public string new_email { get; set; }
}

public class ConfirmEmailQuery : IRequest<Response<LoginDto>>
{
    public required string user_id { get; set; }

    public required string token { get; set; }
}


public class ResendConfirmEmailCommand : IRequest<Response<string>>
{
    public required string Email { get; set; }
}

public class ForgetPasswordCommand : IRequest<Response<string>>
{
    public required string Email { get; set; }
}

public class ResetPasswordCommand : IRequest<Response<string>>
{
    public required string user_id { get; set; }
    public required string Token { get; set; }
    public required string new_password { get; set; }
}

public class ChangePasswordCommand : IRequest<Response<string>>
{
    public required string old_password { get; set; }
    public required string new_password { get; set; }
    public required string confirm_new_password { get; set; }
}

public class RefreshTokenCommand : IRequest<Response<LoginDto>>
{
    [FromRoute]
    public required string RefreshToken { get; set; }
}


public class GetMeQuery : IRequest<Response<UserDto>> { }