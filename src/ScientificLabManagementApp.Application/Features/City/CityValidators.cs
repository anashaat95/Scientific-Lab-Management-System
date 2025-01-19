namespace ScientificLabManagementApp.Application;
public class CityValidator<TCommand> : ValidatorBase<TCommand, City, CityDto>
    where TCommand : AddUpdateCommandBase<CityDto, CityCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }
}


public class AddCityValidator : CityValidator<AddCityCommand> {}

public class UpdateCityValidator : CityValidator<UpdateCityCommand> {}