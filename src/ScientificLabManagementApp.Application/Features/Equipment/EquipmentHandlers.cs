
namespace ScientificLabManagementApp.Application;
public class GetManyEquipmentHandler : GetManyQueryHandlerBase<GetManyEquipmentQuery, Equipment, EquipmentDto>
{
    protected override Task<IEnumerable<EquipmentDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.Company);
    }
}
public class GetOneEquipmentByIdHandler : GetOneQueryHandlerBase<GetOneEquipmentByIdQuery, Equipment, EquipmentDto>
{
    protected override Task<EquipmentDto?> GetEntityDto(GetOneEquipmentByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Company);
    }
}
public class AddEquipmentHandler : AddCommandHandlerBase<AddEquipmentCommand, Equipment, EquipmentDto> { }

public class UpdateEquipmentHandler : UpdateCommandHandlerBase<UpdateEquipmentCommand, Equipment, EquipmentDto> {
    protected readonly IBaseService<Booking, BookingDto> _bookingService;

    public UpdateEquipmentHandler(IBaseService<Booking, BookingDto> bookingService)
    {
        _bookingService = bookingService;
    }

    protected async override Task DoUpdate(UpdateEquipmentCommand updateRequest, Equipment entityToUpdate)
    {
        if (entityToUpdate.Status == enEquipmentStatus.FullyBooked || entityToUpdate.Status == enEquipmentStatus.InMaintenance)
            return;

        var oldEquipmentEntity = await _basicService.GetEntityByIdAsync(updateRequest.Id);

        if (oldEquipmentEntity.Status == updateRequest.Data.Status)
            return;

        if (updateRequest.Data.Status == enEquipmentStatus.Decommissioned || updateRequest.Data.Status == enEquipmentStatus.NotWorking)
        {
            var updatedEntity = _mapper.Map(updateRequest, entityToUpdate);
            updatedEntity.ReservedQuantity = 0;
            await _basicService.UpdateAsync(updatedEntity);

            var bookingEntities = await _bookingService.FindEntitiesAsync(b => b.EquipmentId == updatedEntity.Id);

            foreach (var entity in bookingEntities)
            {
                entity.Status = enBookingStatus.Cancelled;
            }

            await _bookingService.UpdateRangeAsync(bookingEntities);
        }
    }
}

public class DeleteEquipmentHandler : DeleteCommandHandlerBase<DeleteEquipmentCommand, Equipment, EquipmentDto> { }