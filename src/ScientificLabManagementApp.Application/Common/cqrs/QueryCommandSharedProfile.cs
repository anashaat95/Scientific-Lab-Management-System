namespace ScientificLabManagementApp.Application;

public class QueryCommandSharedProfile : Profile
{
    public QueryCommandSharedProfile()
    {
        ApplyQueryToParametersMapping<GetManyBookingQuery, BookingDto>();
        ApplyQueryToParametersMapping<GetManyCityQuery, CityDto>();
        ApplyQueryToParametersMapping<GetManyCompanyQuery, CompanyDto>();
        ApplyQueryToParametersMapping<GetManyCountryQuery, CountryDto>();
        ApplyQueryToParametersMapping<GetManyDepartmentQuery, DepartmentDto>();
        ApplyQueryToParametersMapping<GetManyEquipmentQuery, EquipmentDto>();
        ApplyQueryToParametersMapping<GetManyLabQuery, LabDto>();
        ApplyQueryToParametersMapping<GetManyMaintenanceLogQuery, MaintenanceLogDto>();
        ApplyQueryToParametersMapping<GetManyRoleQuery, RoleDto>();
        ApplyQueryToParametersMapping<GetManyUserQuery, UserDto>();
        ApplyQueryToParametersMapping<GetManyLabSupervisorQuery, UserDto>();
        ApplyQueryToParametersMapping<GetManyResearcherQuery, UserDto>();
        ApplyQueryToParametersMapping<GetManyTechnicianQuery, UserDto>();
    }
    public void ApplyQueryToParametersMapping<TQuery, TDto>()
        where TQuery : GetManyQueryBase<TDto>
        where TDto : class
    {
         CreateMap<TQuery, AllResourceParameters>()
            .ForMember(dest => dest.Filter, opt => opt.MapFrom(src => src.Filter))
            .ForMember(dest => dest.SearchQuery, opt => opt.MapFrom(src => src.SearchQuery))
            .ForMember(dest => dest.SortBy, opt => opt.MapFrom(src => src.SortBy))
            .ForMember(dest => dest.Descending, opt => opt.MapFrom(src => src.Descending))
            .ForMember(dest => dest.PageNumber, opt => opt.MapFrom(src => src.PageNumber))
            .ForMember(dest => dest.PageSize, opt => opt.MapFrom(src => src.PageSize))
            ;
    }
}
