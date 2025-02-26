
namespace ScientificLabManagementApp.Application;
public class EquipmentValidator<TCommand, TData> : ValidatorBase<TCommand, Equipment, EquipmentDto>
    where TCommand : AddUpdateCommandBase<EquipmentDto, TData>
    where TData : EquipmentCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.total_quantity).Must(x => x > 0).WithMessage("total_quantity must be more than zero");
        RuleFor(x => x.Data.Type)
            .Must(type => type is enEquipmentType)
            .WithMessage("Type must be a valid enEquipmentType type.");
        RuleFor(x => x.Data.Status)
            .Must(status => status is enEquipmentStatus)
            .WithMessage("Status must be a valid enEquipmentStatus type.");
        RuleFor(x => x.Data.purchase_date).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.serial_number).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.Specifications).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.Description).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.CanBeLeftOverNight)
            .Must(value => value == true || value == false)
            .WithMessage("Can be left overnight must be either true or false.");
        RuleFor(x => x.Data.company_id).ApplyNotEmptyRule().ApplyNotNullableRule();
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.image_url).ValidateOptionalUrl();

        RuleFor(x => x.Data.company_id)
            .MustAsync(async (id, ct) => await _basicService.RelatedExistsAsync<Company>(id))
            .WithMessage("No company found with the provided {{PropertName}}.");
    }
}
public class AddEquipmentValidator : EquipmentValidator<AddEquipmentCommand, AddEquipmentCommandData> { }

public class UpdateEquipmentValidator : EquipmentValidator<UpdateEquipmentCommand, UpdateEquipmentCommandData> { }