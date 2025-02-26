namespace ScientificLabManagementApp.API;

[Route("api/[controller]")]
[ApiController]
public class EquipmentController :
    ControllerBaseWithEndpoints<
        EquipmentDto, GetOneEquipmentByIdQuery, GetManyEquipmentQuery,
        EquipmentCommandData, AddEquipmentCommandData, UpdateEquipmentCommandData,
        AddEquipmentCommand, UpdateEquipmentCommand, DeleteEquipmentCommand>

{
    [HttpGet("bookings")]
    public async Task<ActionResult<IEnumerable<EquipmentWithBookingsDto>>> getAllWithBookings()
    {
        var response = await Mediator.Send(new GetManyEquipmentWithBookingsQuery());
        return Result.Create(response);
    }

    [HttpGet("options")]
    public async Task<ActionResult<IEnumerable<SelectOptionDto>>> GetAllOptions()
    {
        var response = await Mediator.Send(new GetManyEquipmentSelectOptionsQuery());
        return Result.Create(response);
    }

    // GET api/<Controller>/5
    [HttpGet("{Id}/bookings")]
    public virtual async Task<ActionResult<EquipmentWithBookingsDto>> GetOneWithBookings(GetOneEquipmentWithBookingsByIdQuery command)
    {
        var response = await Mediator.Send(command);
        return Result.Create(response);
    }
}