namespace ScientificLabManagementApp.Application;
public class CityValidator<TCommand, TData> : ValidatorBase<TCommand, City, CityDto>
    where TCommand : AddUpdateCommandBase<CityDto, TData>
    where TData : CityCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }
}


public class AddCityValidator : CityValidator<AddCityCommand, AddCityCommandData> {}

public class UpdateCityValidator : CityValidator<UpdateCityCommand, UpdateCityCommandData> {}