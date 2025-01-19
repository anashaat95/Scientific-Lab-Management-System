namespace ScientificLabManagementApp.Application;

public class CountryCommandData
{
    public required string Name { get; set; }
}

public class AddCountryCommand : AddCommandBase<CountryDto, CountryCommandData>{}

public class UpdateCountryCommand : UpdateCommandBase<CountryDto, CountryCommandData>{}

public class DeleteCountryCommand : DeleteCommandBase<CountryDto>{}