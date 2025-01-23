namespace ScientificLabManagementApp.Application;

public class CompanyCommandData
{
    public required string Name { get; set; }
    public required string Street { get; set; }
    public required string ZipCode { get; set; }
    public required string Website { get; set; }
    public required string city_id { get; set; }
    public required string country_id { get; set; }
}
public class AddCompanyCommand : AddCommandBase<CompanyDto, CompanyCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class UpdateCompanyCommand : UpdateCommandBase<CompanyDto, CompanyCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}

public class DeleteCompanyCommand : DeleteCommandBase<CompanyDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.LabSupervisorLevel;
}