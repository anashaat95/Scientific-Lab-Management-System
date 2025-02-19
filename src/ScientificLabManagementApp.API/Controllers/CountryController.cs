namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CountryController :
    ControllerBaseWithEndpoints<
        CountryDto, GetOneCountryByIdQuery, GetManyCountryQuery,
        CountryCommandData, AddCountryCommandData, UpdateCountryCommandData,
        AddCountryCommand, UpdateCountryCommand, DeleteCountryCommand>
{
    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyCountrySelectOptionsQuery());
        return Result.Create(response);
    }
}