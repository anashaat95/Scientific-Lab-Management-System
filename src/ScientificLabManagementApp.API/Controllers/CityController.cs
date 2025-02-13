namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CityController :
    ControllerBaseWithEndpoints<
        CityDto, GetOneCityByIdQuery, GetManyCityQuery,
        CityCommandData, AddCityCommandData, UpdateCityCommandData,
        AddCityCommand, UpdateCityCommand, DeleteCityCommand>
{
}
