namespace ScientificLabManagementApp.Application;

public class CityCommandData
{
    public required string Name { get; set; }
}

public class AddCityCommand : AddCommandBase<CityDto, CityCommandData>{}

public class UpdateCityCommand : UpdateCommandBase<CityDto, CityCommandData>{}

public class DeleteCityCommand : DeleteCommandBase<CityDto>{}