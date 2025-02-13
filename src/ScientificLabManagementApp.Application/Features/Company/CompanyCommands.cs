namespace ScientificLabManagementApp.Application;

public abstract class CompanyCommandData
{
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }
    public required string Website { get; set; }
    public required string city_id { get; set; }
    public required string country_id { get; set; }
}


public class AddCompanyCommandData : CompanyCommandData { }
public class UpdateCompanyCommandData : CompanyCommandData { }

public class AddCompanyCommand : AddCommandBase<CompanyDto, AddCompanyCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateCompanyCommand : UpdateCommandBase<CompanyDto, UpdateCompanyCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteCompanyCommand : DeleteCommandBase<CompanyDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}