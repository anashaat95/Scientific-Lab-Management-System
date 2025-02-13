namespace ScientificLabManagementApp.Application;
public class CountryValidator<TCommand, TData> : ValidatorBase<TCommand, Country, CountryDto>
    where TCommand : AddUpdateCommandBase<CountryDto, TData>
    where TData : CountryCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }
}

public class AddCountryValidator : CountryValidator<AddCountryCommand, AddCountryCommandData> {}

public class UpdateCountryValidator : CountryValidator<UpdateCountryCommand, UpdateCountryCommandData> {}