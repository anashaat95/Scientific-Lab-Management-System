namespace ScientificLabManagementApp.Application;
public class CompanyMappingProfile : ProfileBase<Company, CompanyDto, CompanyCommandData>
{
    public CompanyMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyEntityToSelectOptionDtoMapping();
        ApplyCommandToEntityMapping<AddCompanyCommand, AddCompanyCommandData>();
        ApplyCommandToEntityMapping<UpdateCompanyCommand, UpdateCompanyCommandData>();
    }


    public override IMappingExpression<Company, CompanyDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<Company, CompanyDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website))
            .ForMember(dest => dest.city_url, opt => opt.MapFrom(src => ApiUrlFactory<City>.Create(src.CityId)))
            .ForMember(dest => dest.city_name, opt => opt.MapFrom(src => src.City.Name))
            .ForMember(dest => dest.country_url, opt => opt.MapFrom(src => ApiUrlFactory<Country>.Create(src.CountryId)))
            .ForMember(dest => dest.country_name, opt => opt.MapFrom(src => src.Country.Name))
            .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
            ;
    }

    public override IMappingExpression<Company, SelectOptionDto> ApplyEntityToSelectOptionDtoMapping()
    {
        return CreateMap<Company, SelectOptionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            ;
    }

    public override IMappingExpression<TSource, Company> ApplyCommandToEntityMapping<TSource, TData>()
    {
        return CreateMap<TSource, Company>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name))
            .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Data.Street))
            .ForMember(dest => dest.ZipCode, opt => opt.MapFrom(src => src.Data.ZipCode))
            .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Data.Website))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.Data.city_id))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.Data.country_id))
            ;
    }
}