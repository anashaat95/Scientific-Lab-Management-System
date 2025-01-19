namespace ScientificLabManagementApp.Application;

public class LabCommandData
{
    public required string Name { get; set; }
    public required int Capacity { get; set; }
    public required TimeOnly opening_time { get; set; }
    public required TimeOnly closing_time { get; set; }
    public string supervisor_id { get; set; }
    public required string department_id { get; set; }
}
public class AddLabCommand : AddCommandBase<LabDto, LabCommandData>{}

public class UpdateLabCommand : UpdateCommandBase<LabDto, LabCommandData>{}

public class DeleteLabCommand : DeleteCommandBase<LabDto>{}