namespace ScientificLabManagementApp.Application;
public class GetManyBookingHandler : GetManyQueryHandlerBase<GetManyBookingQuery, Booking, BookingDto>
{
    protected override Task<IEnumerable<BookingDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.Equipment, e => e.User);
    }
}

public class GetOneBookingByIdHandler : GetOneQueryHandlerBase<GetOneBookingByIdQuery, Booking, BookingDto>
{
    protected override Task<BookingDto?> GetEntityDto(GetOneBookingByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Equipment, e => e.User);
    }
}
public class AddBookingHandler : AddCommandHandlerBase<AddBookingCommand, Booking, BookingDto> { }

public class UpdateBookingHandler : UpdateCommandHandlerBase<UpdateBookingCommand, Booking, BookingDto> { }

public class DeleteBookingHandler : DeleteCommandHandlerBase<DeleteBookingCommand, Booking, BookingDto> { }