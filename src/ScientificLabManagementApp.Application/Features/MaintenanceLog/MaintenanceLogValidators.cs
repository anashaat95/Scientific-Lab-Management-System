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
        RuleFor(x => x.Data.Status)
            .Must(type => type is enMaintenanceStatus)
            .WithMessage("Status must be a valid enMaintenanceStatus type.");
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();




        RuleFor(x => x.Data.technician_id)
            .MustAsync(async (x, ct) =>
            {
                var technician = await _basicService.FindRelatedEntityByIdAsync<ApplicationUser>(e => e.Id == x);
                if (technician == null)
                    throw new FluentValidation.ValidationException($"No technician found with the provided technician_id.");

                var userRoles = await _userManager.GetRolesAsync(technician);

                return userRoles.Contains(enUserRoles.Technician.ToString());
            })
            .WithMessage("The provided user is not a technician.");
    }

}
public class AddMaintenanceLogValidator : MaintenanceLogValidator<AddMaintenanceLogCommand, AddMaintenanceLogCommandData>
{
    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.Data.equipment_id)
            .MustAsync(async (x, ct) =>
            {
                var equipment = await _basicService.FindRelatedEntityByIdAsync<Equipment>(e => e.Id == x);
                if (equipment == null)
                    throw new FluentValidation.ValidationException("No equipment found with the provided equipment_id.");

                if (equipment.Status == enEquipmentStatus.InMaintenance)
                    throw new FluentValidation.ValidationException($"This equipment {equipment.Name} is already in maintenance.");


                if (equipment.Status == enEquipmentStatus.Decommissioned)
                    throw new FluentValidation.ValidationException($"This equipment {equipment.Name} is decommissioned and not available any more.");

                return true;
            })
            .WithMessage("No equipment found with the provided equipment_id.");
    }
}
public class UpdateMaintenanceLogValidator : MaintenanceLogValidator<UpdateMaintenanceLogCommand, UpdateMaintenanceLogCommandData>
{
    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.Data.equipment_id)
            .MustAsync(async (x, ct) =>
            {
                var equipment = await _basicService.FindRelatedEntityByIdAsync<Equipment>(e => e.Id == x);
                if (equipment == null)
                    throw new FluentValidation.ValidationException("No equipment found with the provided equipment_id.");
                return true;
            })
            .WithMessage("No equipment found with the provided equipment_id.");
    }
}
