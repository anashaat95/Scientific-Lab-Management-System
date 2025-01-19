namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CityController :
    ControllerBaseWithEndpoints<
        CityDto, GetOneCityByIdQuery, GetManyCityQuery,
        CityCommandData, AddCityCommand, UpdateCityCommand, DeleteCityCommand>

{
}
