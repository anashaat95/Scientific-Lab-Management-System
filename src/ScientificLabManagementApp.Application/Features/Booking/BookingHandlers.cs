
namespace ScientificLabManagementApp.Application;
public class GetManyBookingHandler : GetManyQueryHandlerBase<GetManyBookingQuery, Booking, BookingDto> { }

public class GetOneBookingByIdHandler : GetOneQueryHandlerBase<GetOneBookingByIdQuery, Booking, BookingDto> { }

public class AddBookingHandler : AddCommandHandlerBase<AddBookingCommand, Booking, BookingDto> { }

public class UpdateBookingHandler : UpdateCommandHandlerBase<UpdateBookingCommand, Booking, BookingDto> { }

public class DeleteBookingHandler : DeleteCommandHandlerBase<DeleteBookingCommand, Booking, BookingDto> { }