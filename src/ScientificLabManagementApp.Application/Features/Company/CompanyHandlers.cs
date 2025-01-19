namespace ScientificLabManagementApp.Application;
public class GetManyCompanyHandler : GetManyQueryHandlerBase<GetManyCompanyQuery, Company, CompanyDto> { }

public class GetOneCompanyByIdHandler : GetOneQueryHandlerBase<GetOneCompanyByIdQuery, Company, CompanyDto> { }

public class AddCompanyHandler : AddCommandHandlerBase<AddCompanyCommand, Company, CompanyDto> { }

public class DeleteCompanyHandler : DeleteCommandHandlerBase<DeleteCompanyCommand, Company, CompanyDto> { }

public class UpdateCompanyHandler : UpdateCommandHandlerBase<UpdateCompanyCommand, Company, CompanyDto> { }
