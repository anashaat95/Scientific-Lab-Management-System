namespace ScientificLabManagementApp.Application;
public class CustomUserValidator<TCommand> : ValidatorBase<TCommand, ApplicationUser, UserDto>
    where TCommand : AddUpdateCommandBase<UserDto, UserCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.UserName).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.first_name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.last_name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.Email).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x.Data.company_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.department_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.lab_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.Data.company_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Company>(x))
            .WithMessage("No company found with the provided company_id");

        RuleFor(x => x.Data.department_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Department>(x))
            .WithMessage("No department found with the provided department_id");

        RuleFor(x => x.Data.lab_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Lab>(x))
            .WithMessage("No lab found with the provided lab_id");

        RuleFor(x => x.Data.image_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.google_scholar_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.academia_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.scopus_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.researcher_gate_url).ValidateOptionalUrl();
    }

}

public class AddUserValidator : CustomUserValidator<AddUserCommand>
{
    public override void ApplyValidationRules()
    {
        base.ApplyValidationRules();
        RuleFor(x => x.Data.Password).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.confirm_password).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x.Data)
            .Must((data) => data.Password == data.confirm_password)
            .WithMessage("There is mismatch between password and confirm password");
    }
}

public class UpdateUserValidator : CustomUserValidator<UpdateUserCommand> { }