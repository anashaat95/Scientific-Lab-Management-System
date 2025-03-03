namespace ScientificLabManagementApp.Application;

public class GetManyCountryQuery : GetManyQueryBase<CountryDto>{}
public class GetManyCountrySelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }

public class GetOneCountryByIdQuery : GetOneQueryBase<CountryDto>{}
