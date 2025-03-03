namespace ScientificLabManagementApp.Application;

public class CityMappingProfile : ProfileBase<City, CityDto, CityCommandData>
{
    public CityMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyEntityToSelectOptionDtoMapping();
        ApplyCommandToEntityMapping<AddCityCommand, AddCityCommandData>();
        ApplyCommandToEntityMapping<UpdateCityCommand, UpdateCityCommandData>();
    }

    public override IMappingExpression<City, CityDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<City, CityDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt));
    }

    public override IMappingExpression<City, SelectOptionDto> ApplyEntityToSelectOptionDtoMapping()
    {
        return CreateMap<City, SelectOptionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            ;
    }
    public override IMappingExpression<TSource, City> ApplyCommandToEntityMapping<TSource, TData>()
    {
        return CreateMap<TSource, City>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name));
    }

}
