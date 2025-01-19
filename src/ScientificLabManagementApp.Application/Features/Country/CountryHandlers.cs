namespace ScientificLabManagementApp.Application;
public class GetManyCountryHandler : GetManyQueryHandlerBase<GetManyCountryQuery, Country, CountryDto> { }

public class GetOneCountryByIdHandler : GetOneQueryHandlerBase<GetOneCountryByIdQuery, Country, CountryDto> { }

public class AddCountryHandler : AddCommandHandlerBase<AddCountryCommand, Country, CountryDto> { }

public class DeleteCountryHandler : DeleteCommandHandlerBase<DeleteCountryCommand, Country, CountryDto> { }

public class UpdateCountryHandler : UpdateCommandHandlerBase<UpdateCountryCommand, Country, CountryDto> { }
