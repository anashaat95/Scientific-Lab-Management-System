
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
    protected async override Task<Response<EquipmentDto>> DoUpdate(UpdateEquipmentCommand updateRequest, Equipment equipmentToUpdate)
    {
        if (updateRequest.Data.Status == enEquipmentStatus.FullyBooked || updateRequest.Data.Status == enEquipmentStatus.InMaintenance
            || equipmentToUpdate.Status == updateRequest.Data.Status)
            return Updated(_mapper.Map<EquipmentDto>(equipmentToUpdate));

        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();
        try
        {
            if (updateRequest.Data.Status == enEquipmentStatus.Decommissioned || updateRequest.Data.Status == enEquipmentStatus.NotWorking)
            {
                var updatedEntity = _mapper.Map(updateRequest, equipmentToUpdate);
                updatedEntity.ReservedQuantity = 0;
                await _unitOfWork.EquipmentRepository.UpdateAsync(updatedEntity);

                // Find and cancel related booking entities
                var bookingEntities = await _unitOfWork.BookingRepository.FindAllAsync(b => b.EquipmentId == updatedEntity.Id);
                foreach (var booking in bookingEntities)
                {
                    booking.Status = enBookingStatus.Cancelled;
                }
                await _unitOfWork.BookingRepository.UpdateRangeAsync(bookingEntities);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();

                var resultDto = _mapper.Map<EquipmentDto>(updatedEntity);
                return Created(resultDto);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return Updated(_mapper.Map<EquipmentDto>(equipmentToUpdate));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<EquipmentDto>($"An error occurred: {ex.Message}");
        }
    }
}

public class DeleteEquipmentHandler : DeleteCommandHandlerBase<DeleteEquipmentCommand, Equipment, EquipmentDto> { }