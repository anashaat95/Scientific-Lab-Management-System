namespace ScientificLabManagementApp.Application;
public class MaintenanceLogValidator<TCommand> : ValidatorBase<TCommand, MaintenanceLog, MaintenanceLogDto>
    where TCommand : AddUpdateCommandBase<MaintenanceLogDto, MaintenanceLogCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Description).ApplyMinMaxLengthRule(ValidationLimitsConfig.DESCRIPTION.MIN, ValidationLimitsConfig.DESCRIPTION.MAX);
        RuleFor(x => x.Data.Status).ApplyNotEmptyRule().ApplyNotNullableRule();

        RuleFor(x => x.Data.equipment_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.technician_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.equipment_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Department>(x))
            .WithMessage("No equipment found with the provided {PropertName}.");

        RuleFor(x => x.Data.technician_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Company>(x))
            .WithMessage("No technician found with the provided {PropertName}.");
    }

}
public class AddMaintenanceLogValidator : MaintenanceLogValidator<AddMaintenanceLogCommand> {}

public class UpdateMaintenanceLogValidator : MaintenanceLogValidator<UpdateMaintenanceLogCommand> {}
