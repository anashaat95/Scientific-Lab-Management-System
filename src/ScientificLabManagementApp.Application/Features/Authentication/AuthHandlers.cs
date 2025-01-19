namespace ScientificLabManagementApp.Application;

public abstract class AuthHandler<TRequest, TDto> : ResponseBuilder, IRequestHandler<TRequest, Response<TDto>>
    where TRequest : IRequest<Response<TDto>>
{
    #region Field(s)
    protected readonly IBaseService<RefreshToken, RefreshToken> _refreshTokenService;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly ITokenService _tokenService;
    protected readonly IEmailService _emailService;
    protected readonly EmailCreator _emailCreator;
    protected readonly IMapper _mapper;
    protected readonly ICurrentUserService _currentUserService;

    #endregion

    #region Constructor
    public AuthHandler()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _refreshTokenService = serviceProvider!.GetRequiredService<IBaseService<RefreshToken, RefreshToken>>();
        _userManager = serviceProvider!.GetRequiredService<UserManager<ApplicationUser>>();
        _tokenService = serviceProvider!.GetRequiredService<ITokenService>();
        _emailService = serviceProvider!.GetRequiredService<IEmailService>();
        _mapper = serviceProvider!.GetRequiredService<IMapper>();
        _emailCreator = serviceProvider!.GetRequiredService<EmailCreator>();
        _currentUserService = serviceProvider!.GetRequiredService<ICurrentUserService>();
    }
    #endregion

    #region HandleRequest
    public abstract Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken);
    #endregion


    protected virtual async Task SendEmail(ApplicationUser user)
    {
        var emailVerificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        _emailCreator.CreateEmailVerificationConfirmationEmail(user, emailVerificationToken);
        await _emailService.SendEmailAsync(_emailCreator.To, _emailCreator.Title, _emailCreator.Body);
    }

    public async Task<LoginDto> CreateLoginDto(ApplicationUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);
        var accessToken = new TokenDto
        {
            Token = _tokenService.GenerateAccessToken(user, userRoles),
            ExpiresIn = DateTime.UtcNow.AddMinutes(_tokenService.AccessTokenExpirationInMinutes)
        };

        var refreshToken = new RefreshToken
        {
            Token = _tokenService.GenerateRefreshToken(),
            ExpiresIn = DateTime.UtcNow.AddDays(_tokenService.RefreshTokenExpirationInDays),
            UserId = user.Id
        };

        var dbRefreshToken = await _refreshTokenService.AddAsync(refreshToken);

        return new LoginDto
        {
            AccessToken = new TokenDto
            {
                Token = _tokenService.GenerateAccessToken(user, userRoles),
                ExpiresIn = DateTime.UtcNow.AddMinutes(_tokenService.AccessTokenExpirationInMinutes)
            },
            RefreshToken = new TokenDto
            {
                Token = refreshToken.Token,
                ExpiresIn = refreshToken.ExpiresIn,
            }
        };
    }
}


public class LoginHandler : AuthHandler<LoginCommand, LoginDto>
{
    #region HandleRequest
    public override async Task<Response<LoginDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            return Unauthorized<LoginDto>("Invalid username and/or password");


        if (!user.EmailConfirmed)
        {
            await SendEmail(user);
            return Ok200<LoginDto>("The entered email is not verified. Please check your email for verification.");
        }

        return Ok200(await CreateLoginDto(user));
    }
    #endregion
}

public class SignupHandler : AuthHandler<SignupCommand, string>
{
    public override async Task<Response<string>> Handle(SignupCommand request, CancellationToken cancellationToken)
    {
        var newUser = _mapper.Map<ApplicationUser>(request);
        newUser.EmailConfirmed = false;

        var creationResult = await _userManager.CreateAsync(newUser, request.Password);

        if (!creationResult.Succeeded)
            return BadRequest<string>($"Failed to add the user. Errors: {creationResult.ConvertErrorsToString()}");


        var roleAssignmentResult = await _userManager.AddToRoleAsync(newUser, enUserRoles.User.ToString());

        if (!roleAssignmentResult.Succeeded)

            return BadRequest<string>($"User created but failed to assign role. Errors: {roleAssignmentResult.ConvertErrorsToString()}");


        await SendEmail(newUser);
        return Ok200<string>("User registered successfully. Please check your email for verification.");
    }
}

public class UpdateDataHandler<TCommand> : AuthHandler<TCommand, string>
    where TCommand : IRequest<Response<string>>
{
    protected string _successMessage { get; set; } = "User profile updated successfully.";


    public override async Task<Response<string>> Handle(TCommand request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.User;
        if (currentUser == null)
            return Unauthorized<string>("Unable to identify the user from the access token.");

        var userFromDb = await _userManager.FindByIdAsync(_currentUserService.UserId);
        if (userFromDb == null)
            return NotFound<string>("User is not found!");

        _mapper.Map(request, userFromDb);

        return await ProcessUserUpdateOrEmailConfirmation(userFromDb, request);
    }

    protected virtual async Task<Response<string>> ProcessUserUpdateOrEmailConfirmation(ApplicationUser user, TCommand request)
    {
        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
            return BadRequest<string>($"Failed to update the user. Errors: {updateResult.ConvertErrorsToString()}");

        return Ok200<string>(_successMessage);
    }
}
public class UpdateProfileHandler : UpdateDataHandler<UpdateProfileCommand> { }

public class UpdateUsernameHandler : UpdateDataHandler<UpdateUsernameCommand>
{
    public UpdateUsernameHandler()
    {
        _successMessage = "Username updated successfully.";
    }
}

public class ChangePasswordHandler : UpdateDataHandler<ChangePasswordCommand>
{
    protected override async Task<Response<string>> ProcessUserUpdateOrEmailConfirmation(ApplicationUser user, ChangePasswordCommand request)
    {
        var result = await _userManager.ChangePasswordAsync(user, request.old_password, request.new_password);

        if (!result.Succeeded)
            return BadRequest<string>("Error!! changing the password");

        return Ok200("Password changed successfully!");
    }
}

public class SendUpdateEmailHandler : UpdateDataHandler<SendUpdateEmailCommand>
{
    protected override async Task<Response<string>> ProcessUserUpdateOrEmailConfirmation(ApplicationUser user, SendUpdateEmailCommand request)
    {
        await SendEmail(user);
        return Ok200<string>("Please check your updated email for verification.");
    }

    protected override async Task SendEmail(ApplicationUser user)
    {
        var emailVerificationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        _emailCreator.CreateUpdateEmailVerificationConfirmationEmail(user, emailVerificationToken);
        await _emailService.SendEmailAsync(_emailCreator.To, _emailCreator.Title, _emailCreator.Body);
    }
}

public class UpdateEmailHandler : AuthHandler<UpdateEmailCommand, string>
{
    public override async Task<Response<string>> Handle(UpdateEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.user_id);

        if (user == null)
            return NotFound<string>("User is not found!");

        _mapper.Map(request, user);

        var result = await _userManager.ConfirmEmailAsync(user, request.token);

        if (!result.Succeeded)
            return BadRequest<string>("Invalid or expired token.");

        return Ok200("Email verified successfully!");
    }
}

public class VerifyEmailHandler : AuthHandler<VerifyEmailQuery, LoginDto>
{
    public override async Task<Response<LoginDto>> Handle(VerifyEmailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.user_id);

        if (user == null)
            return NotFound<LoginDto>("User is not found");

        if (user.EmailConfirmed)
            return Ok200<LoginDto>($"This email is already verified!");

        var result = await _userManager.ConfirmEmailAsync(user, request.token);

        if (!result.Succeeded)
            return BadRequest<LoginDto>("Invalid or expired token.");

        return Ok200(await CreateLoginDto(user), "Email verified successfully!");
    }
}

public class ResendVerificationEmailHandler : AuthHandler<ResendVerifyEmailCommand, string>
{
    public override async Task<Response<string>> Handle(ResendVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return NotFound<string>("User is not found");

        if (user.EmailConfirmed)
            return Ok200<string>($"This email is already verified!");

        await SendEmail(user);

        return Ok200<string>("Please check your email for verification.");
    }
}

public class ForgetPasswordHandler : AuthHandler<ForgetPasswordCommand, string>
{
    public override async Task<Response<string>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return NotFound<string>("User is not found");

        await SendEmail(user);

        return Ok200("Password reset url has been sent to your email.");
    }

    protected override async Task SendEmail(ApplicationUser user)
    {
        var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        _emailCreator.CreatePasswordResetEmail(user, passwordResetToken);
        await _emailService.SendEmailAsync(_emailCreator.To, _emailCreator.Title, _emailCreator.Body);
    }
}

public class ResetPasswordHandler : AuthHandler<ResetPasswordCommand, string>
{
    public override async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
            return NotFound<string>("User is not found");

        var result = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!result.Succeeded)
            return BadRequest<string>("Error!! reseting the password");

        return Ok200("Password reset successfully!");
    }
}

public class RefreshTokenHandler : AuthHandler<RefreshTokenCommand, LoginDto>
{
    public override async Task<Response<LoginDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenService.FindOneAsync(entity => entity.Token == request.RefreshToken && !entity.IsRevoked);

        if (refreshToken == null || refreshToken.IsExpired || refreshToken.IsRevoked)
            return Unauthorized<LoginDto>("Invalid or expired refresh token.");

        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.IsRevoked = true;

        await _refreshTokenService.UpdateAsync(refreshToken);

        var currentUser = _currentUserService.User;
        if (currentUser == null)
            return Unauthorized<LoginDto>("Unable to identify the user from the access token.");

        var userFromDb = await _userManager.FindByIdAsync(_currentUserService.UserId);
        if (userFromDb == null)
            return NotFound<LoginDto>("User is not found!");


        return Ok200(await CreateLoginDto(userFromDb));
    }
}
