namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CompanyController :
    ControllerBaseWithEndpoints<
        CompanyDto, GetOneCompanyByIdQuery, GetManyCompanyQuery,
        CompanyCommandData, AddCompanyCommandData, UpdateCompanyCommandData,
        AddCompanyCommand, UpdateCompanyCommand, DeleteCompanyCommand>

{
}