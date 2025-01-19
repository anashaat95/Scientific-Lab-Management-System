namespace ScientificLabManagementApp.Application;
public class CompanyValidator<TCommand> : ValidatorBase<TCommand, Company, CompanyDto>
    where TCommand : AddUpdateCommandBase<CompanyDto, CompanyCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.Street).ApplyMinMaxLengthRule(ValidationLimitsConfig.ADDRESS.MIN, ValidationLimitsConfig.ADDRESS.MAX);
        RuleFor(x => x.Data.ZipCode).ApplyMinMaxLengthRule(ValidationLimitsConfig.ZIP_CODE.MIN, ValidationLimitsConfig.ZIP_CODE.MAX);
        RuleFor(x => x.Data.Website).ApplyMinMaxLengthRule(ValidationLimitsConfig.URL.MIN, ValidationLimitsConfig.URL.MAX)
                                    .ApplyValidUrlRule();
        RuleFor(x => x.Data.city_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.country_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.city_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<City>(x))
            .WithMessage("No city found with the provided city_id.");
        RuleFor(x => x.Data.country_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Country>(x))
            .WithMessage("No country found with the provided country_id.");
    }
}

public class AddCompanyValidator : CompanyValidator<AddCompanyCommand> {}

public class UpdateCompanyValidator : CompanyValidator<UpdateCompanyCommand> {}