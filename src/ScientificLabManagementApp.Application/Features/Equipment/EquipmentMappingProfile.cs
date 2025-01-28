
namespace ScientificLabManagementApp.Application;
public class EquipmentMappingProfile : ProfileBase<Equipment, EquipmentDto, EquipmentCommandData>
{
    public EquipmentMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyCommandToEntityMapping<AddEquipmentCommand>();
        ApplyCommandToEntityMapping<UpdateEquipmentCommand>();
    }

    public override IMappingExpression<Equipment, EquipmentDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<Equipment, EquipmentDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.total_quantity, opt => opt.MapFrom(src => src.TotalQuantity))
                .ForMember(dest => dest.reserved_quantity, opt => opt.MapFrom(src => src.ReservedQuantity))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.purchase_date, opt => opt.MapFrom(src => src.PurchaseDate))
                .ForMember(dest => dest.serial_number, opt => opt.MapFrom(src => src.SerialNumber))
                .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.Specifications))
                .ForMember(dest => dest.can_be_left_overnight, opt => opt.MapFrom(src => src.CanBeLeftOverNight))
                .ForMember(dest => dest.image_url, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.company_url, opt => opt.MapFrom(src => ApiUrlFactory<Company>.Create(src.CompanyId)))
                .ForMember(dest => dest.company_name, opt => opt.MapFrom(src => src.Company.Name))
                .ForMember(dest => dest.created_at, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.updated_at, opt => opt.MapFrom(src => src.UpdatedAt))
                ;
    }

    public override IMappingExpression<TSource, Equipment> ApplyCommandToEntityMapping<TSource>()
    {
        return CreateMap<TSource, Equipment>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Data.Name))
                .ForMember(dest => dest.TotalQuantity, opt => opt.MapFrom(src => src.Data.total_quantity))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Data.Type))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Data.Status))
                .ForMember(dest => dest.PurchaseDate, opt => opt.MapFrom(src => src.Data.purchase_date))
                .ForMember(dest => dest.SerialNumber, opt => opt.MapFrom(src => src.Data.serial_number))
                .ForMember(dest => dest.Specifications, opt => opt.MapFrom(src => src.Data.Specifications))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Data.Description))
                .ForMember(dest => dest.CanBeLeftOverNight, opt => opt.MapFrom(src => src.Data.can_be_Left_overnight))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Data.image_url))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Data.company_id))
                ;
    }
}