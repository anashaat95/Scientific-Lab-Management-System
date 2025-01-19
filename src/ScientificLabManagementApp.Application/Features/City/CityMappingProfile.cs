
namespace ScientificLabManagementApp.Application;

public class CityMappingProfile : ProfileBase<City, CityDto, CityCommandData>
{
    public CityMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyCommandToEntityMapping<AddCityCommand>();
        ApplyCommandToEntityMapping<UpdateCityCommand>();
    }

    public override IMappingExpression<City, CityDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<City, CityDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt));
    }
    public override IMappingExpression<TSource, City> ApplyCommandToEntityMapping<TSource>()
    {
        return CreateMap<TSource, City>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name));
    }

}
