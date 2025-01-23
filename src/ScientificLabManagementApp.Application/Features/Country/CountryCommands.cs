namespace ScientificLabManagementApp.Application;

public class CountryCommandData
{
    public required string Name { get; set; }
}

public class AddCountryCommand : AddCommandBase<CountryDto, CountryCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateCountryCommand : UpdateCommandBase<CountryDto, CountryCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteCountryCommand : DeleteCommandBase<CountryDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}