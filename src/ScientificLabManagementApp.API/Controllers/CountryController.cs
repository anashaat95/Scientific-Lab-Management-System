namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class CountryController :
    ControllerBaseWithEndpoints<
        CountryDto, GetOneCountryByIdQuery, GetManyCountryQuery,
        CountryCommandData, AddCountryCommand, UpdateCountryCommand, DeleteCountryCommand>

{
}