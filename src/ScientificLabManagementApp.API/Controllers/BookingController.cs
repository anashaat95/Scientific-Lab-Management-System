
namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class BookingController :
    ControllerBaseWithEndpoints<
        BookingDto, GetOneBookingByIdQuery, GetManyBookingQuery,
        BookingCommandData, AddBookingCommandData, UpdateBookingCommandData,
        AddBookingCommand, UpdateBookingCommand, DeleteBookingCommand>
{
}

