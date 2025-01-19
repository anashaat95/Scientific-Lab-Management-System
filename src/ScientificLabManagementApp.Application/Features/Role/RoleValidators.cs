namespace ScientificLabManagementApp.Application;
public class RoleValidator<TCommand> : ValidatorBase<TCommand, ApplicationRole, RoleDto>
    where TCommand : AddUpdateCommandBase<RoleDto, RoleCommandData>
{
    public override void ApplyValidationRules()
    {
        RuleFor(x => x.Data.Name).ApplyMinMaxLengthRule(ValidationLimitsConfig.NAME.MIN, ValidationLimitsConfig.NAME.MAX);
    }
}

public class AddRoleValidator : RoleValidator<AddRoleCommand> {}

public class UpdateRoleValidator : RoleValidator<UpdateRoleCommand> {}