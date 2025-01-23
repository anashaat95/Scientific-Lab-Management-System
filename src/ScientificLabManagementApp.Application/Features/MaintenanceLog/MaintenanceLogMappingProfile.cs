
namespace ScientificLabManagementApp.Application;
public class MaintenanceLogMappingProfile : ProfileBase<MaintenanceLog, MaintenanceLogDto, MaintenanceLogCommandData>
{
    public MaintenanceLogMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyCommandToEntityMapping<AddMaintenanceLogCommand>();
        ApplyCommandToEntityMapping<UpdateMaintenanceLogCommand>();
    }

    public override IMappingExpression<MaintenanceLog, MaintenanceLogDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<MaintenanceLog, MaintenanceLogDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.equipment_name, opt => opt.MapFrom(src => src.Equipment.Name))
                .ForMember(dest => dest.equipment_url, opt => opt.MapFrom(src => ApiUrlFactory<Equipment>.Create(src.EquipmentId)))
                .ForMember(dest => dest.technician_name, opt => opt.MapFrom(src => $"{src.Technician.FirstName} {src.Technician.LastName}"))
                .ForMember(dest => dest.technician_url, opt => opt.MapFrom(src => ApiUrlFactory<ApplicationUser>.Create(src.TechnicianId)))
                .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
                ;
    }
    public override IMappingExpression<TSource, MaintenanceLog> ApplyCommandToEntityMapping<TSource>()
    {
        return CreateMap<TSource, MaintenanceLog>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Data.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Data.Status))
                .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.Data.equipment_id))
                .ForMember(dest => dest.TechnicianId, opt => opt.MapFrom(src => src.Data.technician_id))
                ;
    }
}
