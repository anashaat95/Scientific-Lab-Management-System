namespace ScientificLabManagementApp.Application;
public class DepartmentValidator<TCommand, TData> : ValidatorBase<TCommand, Department, DepartmentDto>
    where TCommand : AddUpdateCommandBase<DepartmentDto, TData>
    where TData : DepartmentCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
        RuleFor(x => x.Data.Location).ApplyMinMaxLengthRule(ValidationLimitsConfig.ADDRESS.MIN, ValidationLimitsConfig.ADDRESS.MAX);
    }

    public override void ApplyCustomValidationRules()
    {
        base.ApplyCustomValidationRules();
        RuleFor(x => x.Data.company_id)
            .MustAsync(async (x, ct) => await _basicService.RelatedExistsAsync<Company>(x))
            .WithMessage("No company found with the provided company_id.");
    }
}
public class AddDepartmentValidator : DepartmentValidator<AddDepartmentCommand, AddDepartmentCommandData> {}

public class UpdateDepartmentValidator : DepartmentValidator<UpdateDepartmentCommand, UpdateDepartmentCommandData> {}