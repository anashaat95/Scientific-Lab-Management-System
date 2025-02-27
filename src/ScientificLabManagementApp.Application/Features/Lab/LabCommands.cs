namespace ScientificLabManagementApp.Application;

public abstract class LabCommandData
{
    public required string Name { get; set; }
    public required int Capacity { get; set; }
    public required TimeOnly opening_time { get; set; }
    public required TimeOnly closing_time { get; set; }
    public string supervisor_id { get; set; }
    public required string department_id { get; set; }
}

public class AddLabCommandData : LabCommandData { }
public class UpdateLabCommandData : LabCommandData { }


public class AddLabCommand : AddCommandBase<LabDto, AddLabCommandData>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateLabCommand : UpdateCommandBase<LabDto, UpdateLabCommandData>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteLabCommand : DeleteCommandBase<LabDto>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}