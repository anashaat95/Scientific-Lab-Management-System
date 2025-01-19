namespace ScientificLabManagementApp.Application;
public class BookingMappingProfile : ProfileBase<Booking, BookingDto, BookingCommandData>
{
    public BookingMappingProfile()
    {
        ApplyEntityToDtoMapping();
        ApplyAddBookingCommandToEntityMapping();
        ApplyUpdateBookingCommandToEntityMapping();
    }

    public override IMappingExpression<Booking, BookingDto> ApplyEntityToDtoMapping()
    {
        return CreateMap<Booking, BookingDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.start_date_time, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(x => x.end_date_time, opt => opt.MapFrom(src => src.EndDateTime))
            .ForMember(x => x.is_on_overnight, opt => opt.MapFrom(src => src.IsOnOverNight))
            .ForMember(x => x.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForMember(x => x.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(x => x.user_id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(x => x.user_name, opt => opt.MapFrom(src => $"{src.User.FirstName} {src.User.LastName}"))
            .ForMember(x => x.equipment_id, opt => opt.MapFrom(src => src.EquipmentId))
            .ForMember(x => x.equipment_name, opt => opt.MapFrom(src => src.Equipment.Name))
            .ForMember(x => x.created_at, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(x => x.updated_at, opt => opt.MapFrom(src => src.UpdatedAt));
    }

    public IMappingExpression<AddBookingCommand, Booking> ApplyAddBookingCommandToEntityMapping()
    {
        return CreateMap<AddBookingCommand, Booking>()
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.Data.start_date_time))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.Data.end_date_time))
            .ForMember(dest => dest.IsOnOverNight, opt => opt.MapFrom(src => src.Data.is_on_overnight))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Data.Notes))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Data.Status))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Data.user_id))
            .ForMember(dest => dest.EquipmentId, opt => opt.MapFrom(src => src.Data.equipment_id))
            .ForMember(dest => dest.SubEquipmentId, opt => opt.MapFrom(src => src.Data.sub_equipment_id));
    }

    public IMappingExpression<UpdateBookingCommand, Booking> ApplyUpdateBookingCommandToEntityMapping()
    {
        return CreateMap<UpdateBookingCommand, Booking>()
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.Data.start_date_time))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.Data.end_date_time))
            .ForMember(dest => dest.IsOnOverNight, opt => opt.MapFrom(src => src.Data.is_on_overnight))
            .ForMember(dest => dest.Notes, opt => opt.MapFrom(src => src.Data.Notes))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Data.Status));
    }
}

