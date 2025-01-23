namespace ScientificLabManagementApp.Application;

public class CityCommandData
{
    public required string Name { get; set; }
}

public class AddCityCommand : AddCommandBase<CityDto, CityCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateCityCommand : UpdateCommandBase<CityDto, CityCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteCityCommand : DeleteCommandBase<CityDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}