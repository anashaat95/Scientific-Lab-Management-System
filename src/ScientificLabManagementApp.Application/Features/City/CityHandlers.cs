namespace ScientificLabManagementApp.Application;
public class GetManyCityHandler : GetManyQueryHandlerBase<GetManyCityQuery, City, CityDto> { }

public class GetOneCityByIdHandler : GetOneQueryHandlerBase<GetOneCityByIdQuery, City, CityDto> { }

public class AddCityHandler : AddCommandHandlerBase<AddCityCommand, City, CityDto> { }

public class DeleteCityHandler : DeleteCommandHandlerBase<DeleteCityCommand, City, CityDto> { }

public class UpdateCityHandler : UpdateCommandHandlerBase<UpdateCityCommand, City, CityDto> { }



