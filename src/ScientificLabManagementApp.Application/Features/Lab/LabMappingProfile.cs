
namespace ScientificLabManagementApp.Application;
public class LabMappingProfile : ProfileBase<Lab, LabDto, LabCommandData>
{
    public LabMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyEntityToSelectOptionDtoMapping();
        ApplyCommandToEntityMapping<AddLabCommand, AddLabCommandData>();
        ApplyCommandToEntityMapping<UpdateLabCommand, UpdateLabCommandData>();
    }

    public override IMappingExpression<Lab, LabDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<Lab, LabDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Capacity))
                .ForMember(dest => dest.opening_time, opt => opt.MapFrom(src => src.OpeningTime))
                .ForMember(dest => dest.closing_time, opt => opt.MapFrom(src => src.ClosingTime))
                .ForMember(dest => dest.supervisor_url, opt => opt.MapFrom(src => ApiUrlFactory<ApplicationUser>.Create(src.SupervisiorId)))
                .ForMember(dest => dest.supervisor_name, opt => opt.MapFrom(src => $"{src.Supervisior.FirstName} {src.Supervisior.LastName}"))
                .ForMember(dest => dest.department_name, opt => opt.MapFrom(src => src.Department.Name))
                .ForMember(dest => dest.department_url, opt => opt.MapFrom(src => ApiUrlFactory<Department>.Create(src.DepartmentId)))
                .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
                ;
    }

    public override IMappingExpression<Lab, SelectOptionDto> ApplyEntityToSelectOptionDtoMapping()
    {
        return CreateMap<Lab, SelectOptionDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            ;
    }
    public override IMappingExpression<TSource, Lab> ApplyCommandToEntityMapping<TSource, TData>()
    {
        return CreateMap<TSource, Lab>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name))
                .ForMember(dest => dest.Capacity, opt => opt.MapFrom(src => src.Data.Capacity))
                .ForMember(dest => dest.OpeningTime, opt => opt.MapFrom(src => src.Data.opening_time))
                .ForMember(dest => dest.ClosingTime, opt => opt.MapFrom(src => src.Data.closing_time))
                .ForMember(dest => dest.SupervisiorId, opt => opt.MapFrom(src => src.Data.supervisor_id))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Data.department_id))
        ;
    }
}
