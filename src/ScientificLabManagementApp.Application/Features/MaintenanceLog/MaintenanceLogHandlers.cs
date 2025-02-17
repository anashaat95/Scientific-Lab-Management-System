using Azure.Core;

namespace ScientificLabManagementApp.Application;
public class GetManyMaintenanceLogHandler : GetManyQueryHandlerBase<GetManyMaintenanceLogQuery, MaintenanceLog, MaintenanceLogDto> { }

public class GetOneMaintenanceLogByIdHandler : GetOneQueryHandlerBase<GetOneMaintenanceLogByIdQuery, MaintenanceLog, MaintenanceLogDto> { }

public class AddMaintenanceLogHandler : AddCommandHandlerBase<AddMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    protected readonly IEquipmentService _equipmentService;

    public AddMaintenanceLogHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }
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

            await _equipmentService.UpdateEquipmentBasedOnMaintenaceStatus(equipment, request.Data.Status);
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
    protected readonly IEquipmentService _equipmentService;

    public UpdateMaintenanceLogHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }
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

            await _equipmentService.UpdateEquipmentBasedOnMaintenaceStatus(equipment, updateRequest.Data.Status);
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
    protected readonly IEquipmentService _equipmentService;

    public DeleteMaintenanceLogHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }
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

            await _equipmentService.UpdateEquipmentIfMaintenaceLogDeleted(equipment);
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

