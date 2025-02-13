namespace ScientificLabManagementApp.Application;
public class RoleValidator<TCommand, TData> : ValidatorBase<TCommand, ApplicationRole, RoleDto>
    where TCommand : AddUpdateCommandBase<RoleDto, TData>
    where TData : RoleCommandData
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }
}

public class AddRoleValidator : RoleValidator<AddRoleCommand, AddRoleCommandData> {}

public class UpdateRoleValidator : RoleValidator<UpdateRoleCommand, UpdateRoleCommandData> {}