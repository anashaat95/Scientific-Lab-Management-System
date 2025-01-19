namespace ScientificLabManagementApp.Application;
public class BookingValidator<TCommand> : ValidatorBase<TCommand, Booking, BookingDto>
    where TCommand : AddUpdateCommandBase<BookingDto, BookingCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.start_date_time).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.end_date_time).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.Notes).ApplyMinMaxLengthRule(0, ValidationLimitsConfig.DESCRIPTION.MAX);
        RuleFor(x => x.Data.Status).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.user_id).ApplyNotEmptyRule().ApplyNotNullableRule();
        RuleFor(x => x.Data.equipment_id).ApplyNotEmptyRule().ApplyNotNullableRule();

    }
    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();

        RuleFor(x => x.Data.user_id)
            .MustAsync(async (user_id, ct) => await _basicService.RelatedExistsAsync<ApplicationUser>(user_id))
            .WithMessage("No researcher found with the provided user_id.");

        // For equipment
        RuleFor(x => x.Data)
                .MustAsync(ValidateEquipmentAsync)
                .WithMessage("No equipment found with the provided equipment_id.");

        // For sub equipment
        RuleFor(x => x.Data)
                .MustAsync(ValidateSubEquipmentAsync)
                .WithMessage("No sub-equipment found with the provided sub_equipment_id.");
    }

    private async Task<bool> ValidateEquipmentAsync(BookingCommandData data, CancellationToken cancellationToken)
    {
        var equipmentEntity = await _basicService.FindRelatedEntityByIdAsync<Equipment>(e => e.Id == data.equipment_id);

        if (equipmentEntity is null) return false;

        if (equipmentEntity.Status == enEquipmentStatus.InMaintainance)
            throw new FluentValidation.ValidationException("Equipment cannot be booked because it is currently in maintenance.");

        if (equipmentEntity.Status == enEquipmentStatus.FullyBooked)
            throw new FluentValidation.ValidationException("Equipment cannot be booked because it is currently fully booked.");

        if (equipmentEntity.Status == enEquipmentStatus.Decommissioned)
            throw new FluentValidation.ValidationException("Equipment cannot be booked because it is decommissioned.");

        if (!equipmentEntity.CanBeLeftOverNight && data.is_on_overnight)
            throw new FluentValidation.ValidationException("Equipment cannot be booked overnight.");

        return true;
    }
    private async Task<bool> ValidateSubEquipmentAsync(BookingCommandData data, CancellationToken cancellationToken)
    {
        if (data.sub_equipment_id == null) return true;

        var subEquipmentEntity = await _basicService.FindRelatedEntityByIdAsync<Equipment>(
            e => e.Id == data.sub_equipment_id && e.ParentEquipmentId == data.equipment_id);

        if (subEquipmentEntity is null) return false;

        if (subEquipmentEntity.Status == enEquipmentStatus.InMaintainance)
            throw new FluentValidation.ValidationException("Sub-equipment cannot be booked because it is currently in maintenance.");

        if (subEquipmentEntity.Status == enEquipmentStatus.FullyBooked)
            throw new FluentValidation.ValidationException("Sub-equipment cannot be booked because it is currently fully booked.");

        if (subEquipmentEntity.Status == enEquipmentStatus.Decommissioned)
            throw new FluentValidation.ValidationException("Sub-equipment cannot be booked because it is decommissioned.");

        return true;
    }
}

public class AddBookingValidator : BookingValidator<AddBookingCommand> { }

public class UpdateBookingValidator : BookingValidator<UpdateBookingCommand> { }
