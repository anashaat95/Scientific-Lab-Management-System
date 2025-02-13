
namespace ScientificLabManagementApp.Application;
public class CountryMappingProfile : ProfileBase<Country, CountryDto, CountryCommandData>
{
    public CountryMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyCommandToEntityMapping<AddCountryCommand, AddCountryCommandData>();
        ApplyCommandToEntityMapping<UpdateCountryCommand, UpdateCountryCommandData>();

    }

    public override IMappingExpression<Country, CountryDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<Country, CountryDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt));
    }

    public override IMappingExpression<TSource, Country> ApplyCommandToEntityMapping<TSource, TData>()
    {
        return CreateMap<TSource, Country>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name));
    }
}