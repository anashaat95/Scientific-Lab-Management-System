namespace ScientificLabManagementApp.Application;
public class LabValidator<TCommand, TData> : ValidatorBase<TCommand, Lab, LabDto>
    where TCommand : AddUpdateCommandBase<LabDto, TData>
    where TData : LabCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.Capacity).Must(capacity => capacity > 0).WithMessage("{PropertyName} should be more than zero");
        RuleFor(x => x.Data.opening_time).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.closing_time).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.supervisor_id).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x)
            .Must(x => x.Data.closing_time > x.Data.opening_time)
            .WithMessage("The lab's closing time must be after its opening time.");
        
        RuleFor(x => x.Data.department_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }
    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.supervisor_id)
            .MustAsync(async (id, ct) => id is null || await _basicService.RelatedExistsAsync<ApplicationUser>(id))
            .WithMessage("No user found with the provided {PropertName}.");

        RuleFor(x => x.Data.department_id)
            .MustAsync(async (id, ct) => await _basicService.RelatedExistsAsync<Department>(id))
            .WithMessage("No department found with the provided {PropertName}.");
    }
}

public class AddLabValidator : LabValidator<AddLabCommand, AddLabCommandData> {}

public class UpdateLabValidator : LabValidator<UpdateLabCommand, UpdateLabCommandData> {}