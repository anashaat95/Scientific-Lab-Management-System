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
public class AddBookingHandler : AddCommandHandlerBase<AddBookingCommand, Booking, BookingDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;

    public AddBookingHandler(IBaseService<Equipment, EquipmentDto> equipmentService)
    {
        _equipmentService = equipmentService;
    }

    public async override Task<Response<BookingDto>> Handle(AddBookingCommand request, CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<Booking>(request);
        var resultDto = await _basicService.AddAsync(entityToAdd);

        if (request.Data.Status == enBookingStatus.Confirmed)
        {
            var equipment = await _equipmentService.GetEntityByIdAsync(request.Data.equipment_id);
            equipment.ReservedQuantity += 1;
            if (equipment.ReservedQuantity >= equipment.TotalQuantity)
            {
                equipment.Status = enEquipmentStatus.FullyBooked;
                equipment.ReservedQuantity = equipment.TotalQuantity;
            }
            await _equipmentService.UpdateAsync(equipment);
        }

        return Created(resultDto);
    }
}
public class UpdateBookingHandler : UpdateCommandHandlerBase<UpdateBookingCommand, Booking, BookingDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;

    public UpdateBookingHandler(IBaseService<Equipment, EquipmentDto> equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected override async Task DoUpdate(UpdateBookingCommand updateRequest, Booking entityToUpdate)
    {
        var oldBookingEntity = await _basicService.GetEntityByIdAsync(updateRequest.Id);

        if (oldBookingEntity.Status == updateRequest.Data.Status) return;


        var updatedEntity = _mapper.Map(updateRequest, entityToUpdate);
        await _basicService.UpdateAsync(updatedEntity);

        var equipment = await _equipmentService.GetEntityByIdAsync(updateRequest.Data.equipment_id);

        if (updatedEntity.Status == enBookingStatus.Confirmed)
        {
            equipment.ReservedQuantity += 1;
            if (equipment.ReservedQuantity == equipment.TotalQuantity)
            {
                equipment.Status = enEquipmentStatus.FullyBooked;
                equipment.ReservedQuantity = equipment.TotalQuantity;
            }
        }
        else if (updateRequest.Data.Status == enBookingStatus.Cancelled || updateRequest.Data.Status == enBookingStatus.Completed)
        {
            equipment.ReservedQuantity -= 1;
            equipment.Status = enEquipmentStatus.Available;

            if (equipment.ReservedQuantity < 0)
            {
                equipment.ReservedQuantity = 0;
            }
        }
        await _equipmentService.UpdateAsync(equipment);
    }

}
public class DeleteBookingHandler : DeleteCommandHandlerBase<DeleteBookingCommand, Booking, BookingDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;

    public DeleteBookingHandler(IBaseService<Equipment, EquipmentDto> equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected async override Task DoDelete(Booking entityToDelete)
    {
        await _basicService.DeleteAsync(entityToDelete);

        var equipment = await _equipmentService.GetEntityByIdAsync(entityToDelete.EquipmentId);

        equipment.ReservedQuantity -= 1;
        equipment.Status = enEquipmentStatus.Available;

        if (equipment.ReservedQuantity < 0)
        {
            equipment.ReservedQuantity = 0;
        }
        await _equipmentService.UpdateAsync(equipment);
    }
}
