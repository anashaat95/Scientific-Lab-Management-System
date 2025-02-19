namespace ScientificLabManagementApp.Application;

public class GetManyCountryQuery : GetManyQueryBases<CountryDto>{}
public class GetManyCountrySelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }

public class GetOneCountryByIdQuery : GetOneQueryBase<CountryDto>{}
