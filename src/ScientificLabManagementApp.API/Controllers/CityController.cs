namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CityController :
    ControllerBaseWithEndpoints<
        CityDto, GetOneCityByIdQuery, GetManyCityQuery,
        CityCommandData, AddCityCommandData, UpdateCityCommandData,
        AddCityCommand, UpdateCityCommand, DeleteCityCommand>
{

    [HttpGet("options")]
    public virtual async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyCitySelectOptionsQuery());
        return Result.Create(response);
    }

}
