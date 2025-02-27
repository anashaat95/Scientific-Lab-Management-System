namespace ScientificLabManagementApp.Application;

public abstract class CityCommandData
{
    public required string Name { get; set; }
}

public class AddCityCommandData : CityCommandData { }
public class UpdateCityCommandData : CityCommandData { }


public class AddCityCommand : AddCommandBase<CityDto, AddCityCommandData>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateCityCommand : UpdateCommandBase<CityDto, UpdateCityCommandData>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteCityCommand : DeleteCommandBase<CityDto>
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.LabSupervisorLevel;
}