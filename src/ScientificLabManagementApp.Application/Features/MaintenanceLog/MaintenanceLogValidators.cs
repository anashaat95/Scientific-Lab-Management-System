namespace ScientificLabManagementApp.Application;
public class MaintenanceLogValidator<TCommand, TData> : ValidatorBase<TCommand, MaintenanceLog, MaintenanceLogDto>
    where TCommand : AddUpdateCommandBase<MaintenanceLogDto, TData>
    where TData : MaintenanceLogCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Description).ApplyMinMaxLengthRule(ValidationLimitsConfig.DESCRIPTION.MIN, ValidationLimitsConfig.DESCRIPTION.MAX);
        RuleFor(x => x.Data.equipment_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.technician_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.equipment_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Equipment>(x))
            .WithMessage("No equipment found with the provided equipment_id.");

        RuleFor(x => x.Data.technician_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<ApplicationUser>(x))
            .WithMessage("No technician found with the provided technician_id.");

        RuleFor(x => x.Data.technician_id)
            .MustAsync(async (x, ct) =>
            {
                var user = await _userManager.FindByIdAsync(x);
                var userRoles = await _userManager.GetRolesAsync(user);

                return userRoles.Contains(enUserRoles.Technician.ToString());
            })
            .WithMessage("No technician found with the provided technician_id.");
    }

}
public class AddMaintenanceLogValidator : MaintenanceLogValidator<AddMaintenanceLogCommand, AddMaintenanceLogCommandData> { }

public class UpdateMaintenanceLogValidator : MaintenanceLogValidator<UpdateMaintenanceLogCommand, UpdateMaintenanceLogCommandData> { }
