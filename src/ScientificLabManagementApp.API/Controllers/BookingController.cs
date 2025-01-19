
namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class BookingController :
    ControllerBaseWithEndpoints<
        BookingDto, GetOneBookingByIdQuery, GetManyBookingQuery,
        BookingCommandData, AddBookingCommand, UpdateBookingCommand, DeleteBookingCommand>
{
}