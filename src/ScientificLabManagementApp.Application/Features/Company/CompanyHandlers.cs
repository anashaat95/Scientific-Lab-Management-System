
namespace ScientificLabManagementApp.Application;
public class GetManyCompanyHandler : GetManyQueryHandlerBase<GetManyCompanyQuery, Company, CompanyDto> {
    protected override Task<IEnumerable<CompanyDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.City, e => e.Country);
    }
}
public class GetManyCompanySelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyCompanySelectOptionsQuery, Company> { }

public class GetOneCompanyByIdHandler : GetOneQueryHandlerBase<GetOneCompanyByIdQuery, Company, CompanyDto> {

    protected override Task<CompanyDto?> GetEntityDto(GetOneCompanyByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.City, e => e.Country);
    }
}

public class AddCompanyHandler : AddCommandHandlerBase<AddCompanyCommand, Company, CompanyDto> { }

public class DeleteCompanyHandler : DeleteCommandHandlerBase<DeleteCompanyCommand, Company, CompanyDto> { }

public class UpdateCompanyHandler : UpdateCommandHandlerBase<UpdateCompanyCommand, Company, CompanyDto> { }
