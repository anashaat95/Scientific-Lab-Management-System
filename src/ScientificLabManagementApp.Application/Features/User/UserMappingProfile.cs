namespace ScientificLabManagementApp.Application;

public class UserMappingProfile : ProfileBase<ApplicationUser, UserDto, UserCommandData>
{
    public UserMappingProfile()
    {
        ApplyCustomEntityToDtoMapping();
        ApplyCustomEntityToSelectOptionDtoMapping();
        ApplyCommandToEntityMapping<AddUserCommand, AddUserCommandData>();
        ApplyCommandToEntityMapping<UpdateUserCommand, UpdateUserCommandData>();
    }

    public override IMappingExpression<ApplicationUser, UserDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<ApplicationUser, UserDto>();
    }

    public IMappingExpression<MappingApplicationUser, UserDto> ApplyCustomEntityToDtoMapping()
    {
        return CreateMap<MappingApplicationUser, UserDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.first_name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.last_name, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.email_confirmed, opt => opt.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.phone_number, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.two_factor_enabled, opt => opt.MapFrom(src => src.TwoFactorEnabled))
                .ForMember(dest => dest.image_url, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.company_url, opt => opt.MapFrom(src => ApiUrlFactory<Company>.Create(src.CompanyId)))
                .ForMember(dest => dest.company_name, opt => opt.MapFrom(src => src.CompanyName))
                .ForMember(dest => dest.department_url, opt => opt.MapFrom(src => ApiUrlFactory<Department>.Create(src.DepartmentId)))
                .ForMember(dest => dest.department_name, opt => opt.MapFrom(src => src.DepartmentName))
                .ForMember(dest => dest.lab_url, opt => opt.MapFrom(src => ApiUrlFactory<Lab>.Create(src.LabId)))
                .ForMember(dest => dest.lab_name, opt => opt.MapFrom(src => src.LabName))
                .ForMember(dest => dest.roles, opt => opt.MapFrom(src => src.Roles))
                .ForMember(dest => dest.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
                .ForMember(dest => dest.google_scholar_url, opt => opt.MapFrom(src => src.GoogleScholarUrl))
                .ForMember(dest => dest.academia_url, opt => opt.MapFrom(src => src.AcademiaUrl))
                .ForMember(dest => dest.scopus_url, opt => opt.MapFrom(src => src.ScopusUrl))
                .ForMember(dest => dest.researcher_gate_url, opt => opt.MapFrom(src => src.ResearcherGateUrl))
                .ForMember(dest => dest.expertise_area, opt => opt.MapFrom(src => src.ExpertiseArea))
                .ForMember(dest => dest.created_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
                ;
    }

    public IMappingExpression<MappingApplicationUserSelectOption, SelectOptionDto> ApplyCustomEntityToSelectOptionDtoMapping()
    {
        return CreateMap<MappingApplicationUserSelectOption, SelectOptionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName))
            ;
    }


    public override IMappingExpression<TSource, ApplicationUser> ApplyCommandToEntityMapping<TSource, TData>()
    {
        return CreateMap<TSource, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Data.UserName))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Data.first_name))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Data.last_name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Data.Email))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Data.phone_number))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Data.image_url))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Data.company_id))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Data.department_id))
                .ForMember(dest => dest.LabId, opt => opt.MapFrom(src => src.Data.lab_id))
                .ForMember(dest => dest.GoogleScholarUrl, opt => opt.MapFrom(src => src.Data.google_scholar_url))
                .ForMember(dest => dest.AcademiaUrl, opt => opt.MapFrom(src => src.Data.academia_url))
                .ForMember(dest => dest.ScopusUrl, opt => opt.MapFrom(src => src.Data.scopus_url))
                .ForMember(dest => dest.ResearcherGateUrl, opt => opt.MapFrom(src => src.Data.researcher_gate_url))
                .ForMember(dest => dest.ExpertiseArea, opt => opt.MapFrom(src => src.Data.expertise_area))
                ;
    }

}