namespace ScientificLabManagementApp.Application;
public class CountryValidator<TCommand> : ValidatorBase<TCommand, Country, CountryDto>
    where TCommand : AddUpdateCommandBase<CountryDto, CountryCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }
}

public class AddCountryValidator : CountryValidator<AddCountryCommand> {}

public class UpdateCountryValidator : CountryValidator<UpdateCountryCommand> {}