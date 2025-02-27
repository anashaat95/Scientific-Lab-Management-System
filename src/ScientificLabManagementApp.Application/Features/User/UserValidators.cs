namespace ScientificLabManagementApp.Application;
public class UserValidator<TCommand, TData> : ValidatorBase<TCommand, ApplicationUser, UserDto>
    where TData : UserCommandData
    where TCommand : AddUpdateCommandBase<UserDto, TData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.UserName).ApplyNotEmptyRule().ApplyNotNullableRule().ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.first_name).ApplyNotEmptyRule().ApplyNotNullableRule().ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.last_name).ApplyNotEmptyRule().ApplyNotNullableRule().ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.Email).ApplyNotEmptyRule().ApplyNotNullableRule().ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x.Data.company_id).ApplyNotEmptyRule().ApplyNotNullableRule().ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.department_id).ApplyNotEmptyRule().ApplyNotNullableRule().ApplyNotEmptyRule().ApplyNotNullableRule();
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


        RuleFor(x => x.Data.google_scholar_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.google_scholar_url).ValidateKeywordInUrl("scholar.google.com");
        

        RuleFor(x => x.Data.academia_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.academia_url).ValidateKeywordInUrl("academia.edu");

        RuleFor(x => x.Data.scopus_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.scopus_url).ValidateKeywordInUrl("scopus.com");

        RuleFor(x => x.Data.researcher_gate_url).ValidateOptionalUrl();
        RuleFor(x => x.Data.researcher_gate_url).ValidateKeywordInUrl("researchgate.net");
    }
}

public class AddUserValidator : UserValidator<AddUserCommand, AddUserCommandData>
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

public class UpdateUserValidator : UserValidator<UpdateUserCommand,UpdateUserCommandData> { }