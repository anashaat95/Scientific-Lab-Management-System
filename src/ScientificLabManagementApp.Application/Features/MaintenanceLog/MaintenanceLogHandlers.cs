namespace ScientificLabManagementApp.Application;
public class GetManyMaintenanceLogHandler : GetManyQueryHandlerBase<GetManyMaintenanceLogQuery, MaintenanceLog, MaintenanceLogDto> { }

public class GetOneMaintenanceLogByIdHandler : GetOneQueryHandlerBase<GetOneMaintenanceLogByIdQuery, MaintenanceLog, MaintenanceLogDto> { }

public class AddMaintenanceLogHandler : AddCommandHandlerBase<AddMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;
    protected readonly IBaseService<Booking, BookingDto> _bookingService;

    public AddMaintenanceLogHandler(IBaseService<Equipment, EquipmentDto> equipmentService, IBaseService<Booking, BookingDto> bookingService)
    {
        _equipmentService = equipmentService;
        _bookingService = bookingService;
    }
    public async override Task<Response<MaintenanceLogDto>> Handle(AddMaintenanceLogCommand request, CancellationToken cancellationToken)
    {
        var maintenanceLog = _mapper.Map<MaintenanceLog>(request);
        var resultDto = await _basicService.AddAsync(maintenanceLog);

        var equipment = await _equipmentService.GetEntityByIdAsync(request.Data.equipment_id);
        if (request.Data.Status == enMaintenanceStatus.InMaintenance)
        {
            equipment.Status = enEquipmentStatus.InMaintenance;
            equipment.ReservedQuantity = 0;
            var bookingEntities = await _bookingService.FindEntitiesAsync(b => b.EquipmentId == equipment.Id);

            foreach (var entity in bookingEntities)
            {
                entity.Status = enBookingStatus.Cancelled;
            }

            await _bookingService.UpdateRangeAsync(bookingEntities);
        }
        else
        {
            equipment.Status = enEquipmentStatus.Available;
            if (equipment.ReservedQuantity == equipment.TotalQuantity)
                equipment.Status = enEquipmentStatus.FullyBooked;
        }
        await _equipmentService.UpdateAsync(equipment);

        return Created(resultDto);
    }
}
public class UpdateMaintenanceLogHandler : UpdateCommandHandlerBase<UpdateMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;
    protected readonly IBaseService<Booking, BookingDto> _bookingService;

    public UpdateMaintenanceLogHandler(IBaseService<Equipment, EquipmentDto> equipmentService, IBaseService<Booking, BookingDto> bookingService)
    {
        _equipmentService = equipmentService;
        _bookingService = bookingService;
    }

    protected async override Task DoUpdate(UpdateMaintenanceLogCommand updateRequest, MaintenanceLog entityToUpdate)
    {
        var oldMaintenanceLog = await _basicService.GetEntityByIdAsync(entityToUpdate.Id);

        if (oldMaintenanceLog.Status == updateRequest.Data.Status)
            return;

        var maintenanceLog = _mapper.Map(updateRequest, entityToUpdate);
        await _basicService.UpdateAsync(maintenanceLog);

        var equipment = await _equipmentService.GetEntityByIdAsync(updateRequest.Data.equipment_id);
        if (updateRequest.Data.Status == enMaintenanceStatus.InMaintenance)
        {
            equipment.Status = enEquipmentStatus.InMaintenance;
        }
        else
        {
            equipment.Status = enEquipmentStatus.Available;
            if (equipment.ReservedQuantity == equipment.TotalQuantity)
                equipment.Status = enEquipmentStatus.FullyBooked;
        }
        await _equipmentService.UpdateAsync(equipment);
    }
}

public class DeleteMaintenanceLogHandler : DeleteCommandHandlerBase<DeleteMaintenanceLogCommand, MaintenanceLog, MaintenanceLogDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;

    public DeleteMaintenanceLogHandler(IBaseService<Equipment, EquipmentDto> equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected async override Task DoDelete(MaintenanceLog maintenanceLogToDelete)
    {
        await _basicService.DeleteAsync(maintenanceLogToDelete);

        var equipment = await _equipmentService.GetEntityByIdAsync(maintenanceLogToDelete.EquipmentId);

        equipment.Status = enEquipmentStatus.Available;
        if (equipment.ReservedQuantity == equipment.TotalQuantity)
            equipment.Status = enEquipmentStatus.FullyBooked;

        await _equipmentService.UpdateAsync(equipment);
    }
}

