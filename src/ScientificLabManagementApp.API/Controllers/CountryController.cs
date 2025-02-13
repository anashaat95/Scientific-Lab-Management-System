namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CountryController :
    ControllerBaseWithEndpoints<
        CountryDto, GetOneCountryByIdQuery, GetManyCountryQuery,
        CountryCommandData, AddCountryCommandData, UpdateCountryCommandData,
        AddCountryCommand, UpdateCountryCommand, DeleteCountryCommand>
{
}