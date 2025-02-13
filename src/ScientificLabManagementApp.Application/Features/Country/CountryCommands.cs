namespace ScientificLabManagementApp.Application;

public abstract class CountryCommandData
{
    public required string Name { get; set; }
}

public class AddCountryCommandData : CountryCommandData { }
public class UpdateCountryCommandData : CountryCommandData { }

public class AddCountryCommand : AddCommandBase<CountryDto, AddCountryCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateCountryCommand : UpdateCommandBase<CountryDto, UpdateCountryCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteCountryCommand : DeleteCommandBase<CountryDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}