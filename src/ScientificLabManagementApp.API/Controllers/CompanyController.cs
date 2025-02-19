namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CompanyController :
    ControllerBaseWithEndpoints<
        CompanyDto, GetOneCompanyByIdQuery, GetManyCompanyQuery,
        CompanyCommandData, AddCompanyCommandData, UpdateCompanyCommandData,
        AddCompanyCommand, UpdateCompanyCommand, DeleteCompanyCommand>

{
    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyCompanySelectOptionsQuery());
        return Result.Create(response);
    }
}