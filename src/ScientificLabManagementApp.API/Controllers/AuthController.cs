namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<LoginDto>> Login(LoginCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpPost("signup")]
    public async Task<ActionResult<string>> Signup(SignupCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpPut("update-profile")]
    public async Task<ActionResult<string>> UpdateProfile(UpdateProfileCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpPut("update-username")]
    public async Task<ActionResult<string>> UpdateUsername(UpdateUsernameCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpGet("verify-email")]
    public async Task<ActionResult<LoginDto>> VerifyEmail([FromQuery] VerifyEmailQuery command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }


    [HttpPut("update-email")]
    public async Task<ActionResult<string>> UpdateEmail(SendUpdateEmailCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }


    [HttpPost("verify-update-email")]
    public async Task<ActionResult<string>> UpdateEmail([FromQuery]UpdateEmailCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }


    [HttpPost("resend-verification-for-email")]
    public async Task<ActionResult<string>> ResendVerificationForEmail(ResendVerifyEmailCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }


    [HttpPost("forget-password")]
    public async Task<ActionResult<LoginDto>> ResetPassword(ForgetPasswordCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpPost("reset-password")]
    public async Task<ActionResult<LoginDto>> ResetPassword(ResetPasswordCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpPut("update-password")]
    public async Task<ActionResult<LoginDto>> ChangePassword(ChangePasswordCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginDto>> RefreshToken(RefreshTokenCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }
}