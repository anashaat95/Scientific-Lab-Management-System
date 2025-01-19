namespace ScientificLabManagementApp.Application;
public class EquipmentValidator<TCommand> : ValidatorBase<TCommand, Equipment, EquipmentDto>
    where TCommand : AddUpdateCommandBase<EquipmentDto, EquipmentCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.total_quantity).Must(x => x > 0).WithMessage("total_quantity must be more than zero");
        RuleFor(x => x.Data.Type).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.Status).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.purchase_date).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.serial_number).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.Specifications).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.Description).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.can_be_Left_overnight).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.company_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.image_url).ValidateOptionalUrl();

        RuleFor(x => x.Data.parent_equipment_id)
            .MustAsync(async (id, ct) =>
            {
                if (String.IsNullOrEmpty(id) || String.IsNullOrWhiteSpace(id)) return true;
                return await _basicService.RelatedExistsAsync<Equipment>(id);
            })
            .WithMessage("No parent equipment with the provided parent_equipment_id.");

        RuleFor(x => x.Data.company_id)
            .MustAsync(async (id, ct) => await _basicService.RelatedExistsAsync<Company>(id))
            .WithMessage("No company found with the provided {{PropertName}}.");
    }
}
public class AddEquipmentValidator : EquipmentValidator<AddEquipmentCommand> { }

public class UpdateEquipmentValidator : EquipmentValidator<UpdateEquipmentCommand> { }