using Azure.Core;
using MediatR;

namespace ScientificLabManagementApp.Application;
public class GetManyMaintenanceLogHandler : GetManyQueryHandlerBase<GetManyMaintenanceLogQuery, MaintenanceLog, MaintenanceLogDto> { }

public class GetOneMaintenanceLogByIdHandler : GetOneQueryHandlerBase<GetOneMaintenanceLogByIdQuery, MaintenanceLog, MaintenanceLogDto> { }

public class AddMaintenanceLogHandler : AddCommandHandlerBase<AddMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    public async override Task<Response<MaintenanceLogDto>> Handle(AddMaintenanceLogCommand request, CancellationToken cancellationToken)
    {
        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();

        try
        {
            // Map and Add Maintenance Log
            var maintenanceLog = _mapper.Map<MaintenanceLog>(request);
            var resultEntity = await _unitOfWork.MaintenanceLogRepository.AddAsync(maintenanceLog);
            await _unitOfWork.SaveChangesAsync();

            // Get Equipment
            var equipment = await _unitOfWork.EquipmentRepository.GetOneByIdAsync(request.Data.equipment_id);
            if (equipment == null)
                return NotFound<MaintenanceLogDto>("Equipment not found");

            if (request.Data.Status == enMaintenanceStatus.InMaintenance)
            {
                // Update Equipment Status
                equipment.Status = enEquipmentStatus.InMaintenance;
                equipment.ReservedQuantity = 0;

                // Cancel Bookings
                var bookingEntities = await _unitOfWork.BookingRepository.FindAllAsync(b => b.EquipmentId == equipment.Id);
                foreach (var entity in bookingEntities)
                {
                    entity.Status = enBookingStatus.Cancelled;
                }
                await _unitOfWork.BookingRepository.UpdateRangeAsync(bookingEntities);
            }
            else if (request.Data.Status == enMaintenanceStatus.Fixed)
            {
                // Update Equipment Status if not in maintenance
                equipment.Status = enEquipmentStatus.Available;
                equipment.ReservedQuantity = 0;
            }

            await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return Created(_mapper.Map<MaintenanceLogDto>(resultEntity));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<MaintenanceLogDto>($"An error occurred: {ex.Message}");
        }
    }
}
public class UpdateMaintenanceLogHandler : UpdateCommandHandlerBase<UpdateMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    protected async override Task<Response<MaintenanceLogDto>> DoUpdate(UpdateMaintenanceLogCommand updateRequest, MaintenanceLog entityToUpdate)
    {
        if (entityToUpdate.Status == updateRequest.Data.Status)
            return Updated<MaintenanceLogDto>(_mapper.Map<MaintenanceLogDto>(entityToUpdate));

        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();

        try
        {
            var updatedMaintenanceLog = _mapper.Map(updateRequest, entityToUpdate);
            var resultEntity = await _unitOfWork.MaintenanceLogRepository.UpdateAsync(updatedMaintenanceLog);
            await _unitOfWork.SaveChangesAsync();


            // Get Equipment
            var equipment = await _unitOfWork.EquipmentRepository.GetOneByIdAsync(updateRequest.Data.equipment_id);
            if (equipment == null)
                return NotFound<MaintenanceLogDto>("Equipment not found");

            if (updateRequest.Data.Status == enMaintenanceStatus.InMaintenance)
            {
                equipment.Status = enEquipmentStatus.InMaintenance;
                equipment.ReservedQuantity = 0;

                // Cancel Bookings
                var bookingEntities = await _unitOfWork.BookingRepository.FindAllAsync(b => b.EquipmentId == equipment.Id);
                foreach (var entity in bookingEntities)
                {
                    entity.Status = enBookingStatus.Cancelled;
                }
                await _unitOfWork.BookingRepository.UpdateRangeAsync(bookingEntities);
            }
            else if (updateRequest.Data.Status == enMaintenanceStatus.Fixed)
            {
                equipment.Status = enEquipmentStatus.Available;
                equipment.ReservedQuantity = 0;
            }

            await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return Created(_mapper.Map<MaintenanceLogDto>(resultEntity));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<MaintenanceLogDto>($"An error occurred: {ex.Message}");
        }
    }
}

public class DeleteMaintenanceLogHandler : DeleteCommandHandlerBase<DeleteMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    protected async override Task<Response<MaintenanceLogDto>> DoDelete(MaintenanceLog maintenanceLogToDelete)
    {
        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();

        try
        {
            await _unitOfWork.MaintenanceLogRepository.DeleteAsync(maintenanceLogToDelete); 
            await _unitOfWork.SaveChangesAsync();

            // Get Equipment
            var equipment = await _unitOfWork.EquipmentRepository.GetOneByIdAsync(maintenanceLogToDelete.EquipmentId);
            if (equipment == null)
                return NotFound<MaintenanceLogDto>("Equipment not found");

            equipment.Status = enEquipmentStatus.Available;
            equipment.ReservedQuantity = 0;
            await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return Deleted<MaintenanceLogDto>();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<MaintenanceLogDto>($"An error occurred: {ex.Message}");
        }
    }
}

