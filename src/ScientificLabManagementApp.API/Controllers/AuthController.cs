using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

    [HttpPost("confirm-email")]
    public async Task<ActionResult<LoginDto>> VerifyEmail([FromQuery] ConfirmEmailQuery command)
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


    [HttpPost("confirm-updated-email")]
    public async Task<ActionResult<string>> UpdateEmail([FromQuery]UpdateEmailCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }


    [HttpPost("resend-confirmation-for-email")]
    public async Task<ActionResult<string>> ResendVerificationForEmail(ResendConfirmEmailCommand command)
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

    [HttpPost("refresh-token/{RefreshToken}")]
    public async Task<ActionResult<LoginDto>> RefreshToken(RefreshTokenCommand command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }

    // GET
    [HttpGet("/api/me")]
    public virtual async Task<ActionResult<UserDto>> GetMe()
    {
        var response = await Mediator.Send(new GetMeQuery());
        return Result.Create(response);
    }
}