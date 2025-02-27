namespace ScientificLabManagementApp.Application;

public abstract class AuthValidatorBase<TQueryOrCommand> : AbstractValidator<TQueryOrCommand>
    where TQueryOrCommand : class
{
    #region Fields
    protected readonly IBaseService<ApplicationUser, UserDto> _userService;
    protected readonly UserManager<ApplicationUser> _userManager;
    #endregion

    #region Constructor(s)
    public AuthValidatorBase()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _userService = serviceProvider!.GetRequiredService<IBaseService<ApplicationUser, UserDto>>();
        _userManager = serviceProvider!.GetRequiredService<UserManager<ApplicationUser>>();
        ApplyValidationRules();
        ApplyCustomValidationRules();
    }
    #endregion

    #region Actions
    public abstract void ApplyValidationRules();
    public virtual void ApplyCustomValidationRules() { }
    #endregion
}

public class LoginValidator : AuthValidatorBase<LoginCommand>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Email).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Password).ApplyNotEmptyRule().ApplyNotNullableRule();
    }
    #endregion
}

public class SignupValidator : AuthValidatorBase<SignupCommand>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.UserName).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.first_name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.last_name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Email).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.company_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.department_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.lab_id).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x.Password).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.confirm_password).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x)
            .Must((data) => data.Password.Trim() == data.confirm_password.Trim())
            .WithMessage("There is mismatch between password and confirm password");
    }

    public override void ApplyCustomValidationRules()
    {
        RuleFor(x => x.Email)
            .MustAsync(async (x, ct) => { 
                Console.WriteLine(x);
                return await _userManager.FindByEmailAsync(x) == null;
            })
            .WithMessage($"There is a user with this email. Please, try to login.");

        RuleFor(x => x.company_id)
            .MustAsync(async (x, ct) => await _userService.RelatedExistsAsync<Company>(x))
            .WithMessage("No company found with the provided company_id");

        RuleFor(x => x.department_id)
            .MustAsync(async (x, ct) => await _userService.RelatedExistsAsync<Department>(x))
            .WithMessage("No department found with the provided department_id");

        RuleFor(x => x.lab_id)
            .MustAsync(async (x, ct) => await _userService.RelatedExistsAsync<Lab>(x))
            .WithMessage("No lab found with the provided lab_id");

        RuleFor(x => x.google_scholar_url).ValidateOptionalUrl();
        RuleFor(x => x.academia_url).ValidateOptionalUrl();
        RuleFor(x => x.scopus_url).ValidateOptionalUrl();
        RuleFor(x => x.researcher_gate_url).ValidateOptionalUrl();
    }

}



public class VerifyEmailValidator : AuthValidatorBase<ConfirmEmailQuery>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.user_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.token).ApplyNotEmptyRule().ApplyNotNullableRule();
    }
    #endregion
}

public class ResendVerifyEmailValidator : AuthValidatorBase<ResendConfirmEmailCommand>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Email).ApplyNotEmptyRule().ApplyNotNullableRule();
    }
    #endregion
}



public class UpdateProfileValidator : AuthValidatorBase<UpdateProfileCommand>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.first_name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.last_name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }

    public override void ApplyCustomValidationRules()
    {
        RuleFor(x => x.google_scholar_url).ValidateOptionalUrl();
        RuleFor(x => x.academia_url).ValidateOptionalUrl();
        RuleFor(x => x.scopus_url).ValidateOptionalUrl();
        RuleFor(x => x.researcher_gate_url).ValidateOptionalUrl();
    }

}

public class UpdateUsernameValidator : AuthValidatorBase<UpdateUsernameCommand>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Username).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Username)
            .MustAsync(async (username, ct) => await _userService.FindOneAsync(user => user.UserName == username) == null)
            .WithMessage("There is a user with this username. Please, try to use another username.");
    }
}

public class SendUpdateEmailValidator : AuthValidatorBase<SendUpdateEmailCommand>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.new_email).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.new_email)
            .MustAsync(async (x, ct) => await _userManager.FindByEmailAsync(x) == null)
            .WithMessage($"There is a user with this email. Please, try to login.");
    }
}

public class UpdateEmailValidator : AuthValidatorBase<UpdateEmailCommand>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.user_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.token).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.new_email).ApplyNotEmptyRule().ApplyNotNullableRule();
    }
    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.new_email)
            .MustAsync(async (x, ct) => await _userManager.FindByEmailAsync(x) == null)
            .WithMessage($"There is a user with this email. Please, try to login.");
    }
}


public class ForgetPasswordValidator : AuthValidatorBase<ForgetPasswordCommand>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Email).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.Email)
            .MustAsync(async (x, ct) => await _userManager.FindByEmailAsync(x) != null)
            .WithMessage($"There is no user with this email.");
    }
    #endregion
}

public class ResetPasswordValidator : AuthValidatorBase<ResetPasswordCommand>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.user_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Token).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.new_password).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();


        RuleFor(x => x.user_id)
            .MustAsync(async (x, ct) => await _userManager.FindByIdAsync(x) != null)
            .WithMessage($"User is not found.");
    }
    #endregion
}

public class ChangePasswordValidator : AuthValidatorBase<ChangePasswordCommand>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.old_password).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.new_password).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.confirm_new_password).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x)
            .Must((data) => data.new_password.Trim() == data.confirm_new_password.Trim())
            .WithMessage("There is mismatch between NewPassword dand NewConfirmPassword");
    }
    #endregion
}
public class RefreshTokenValidator : AuthValidatorBase<RefreshTokenCommand>
{
    #region Actions
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.RefreshToken).ApplyNotEmptyRule().ApplyNotNullableRule();
    }
    #endregion
}