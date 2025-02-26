namespace ScientificLabManagementApp.Application;

public class EquipmentService : IEquipmentService
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public EquipmentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(IUnitOfWork)); ;
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); ;
    }

    public virtual async Task<EquipmentWithBookingsDto> GetEquipmentWithBookingsDtoByIdAsync(string id, params Expression<Func<Equipment, object>>[] includes)
    {
        var result = await _unitOfWork.EquipmentRepository
            .GetQueryableEntityAsync(e => e.Id == id)
            .Include(e => e.Company)
            .Include(e => e.Bookings)
            .ThenInclude(e=>e.User)
            .FirstOrDefaultAsync();
        return _mapper.Map<EquipmentWithBookingsDto>(result);
    }

    public async Task<Response<IEntityHaveId>> UpdateEquipmentIfBookingConfirmed(Equipment equipment)
    {
        equipment.ReservedQuantity += 1;

        await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
        await _unitOfWork.SaveChangesAsync();

        return Response<IEntityHaveId>.Success();
    }

    public async Task<Response<IEntityHaveId>> UpdateEquipmentIfBookingDeleted(Equipment equipment)
    {

        equipment.Status = enEquipmentStatus.Available;
        equipment.ReservedQuantity -= 1;

        if (equipment.ReservedQuantity < 0) equipment.ReservedQuantity = 0;

        await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
        await _unitOfWork.SaveChangesAsync();

        return Response<IEntityHaveId>.Success();
    }

    public async Task<Response<IEntityHaveId>> UpdateEquipmentBasedOnMaintenaceStatus(Equipment equipment, enMaintenanceStatus maintenanceStatus)
    {

        equipment.ReservedQuantity = 0;
        if (maintenanceStatus == enMaintenanceStatus.InMaintenance)
        {
            // Update Equipment Status
            equipment.Status = enEquipmentStatus.InMaintenance;
            await CancelAllBookingsRelatedToEquipment(equipment.Id);
        }
        else if (maintenanceStatus == enMaintenanceStatus.Fixed)
        {
            equipment.Status = enEquipmentStatus.Available;
        }

        await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
        await _unitOfWork.SaveChangesAsync();

        return Response<IEntityHaveId>.Success();
    }

    public async Task<Response<IEntityHaveId>> UpdateEquipmentIfMaintenaceLogDeleted(Equipment equipment)
    {

        equipment.Status = enEquipmentStatus.Available;
        equipment.ReservedQuantity = 0;

        await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
        await _unitOfWork.SaveChangesAsync();

        return Response<IEntityHaveId>.Success();
    }

    public async Task<Response<IEntityHaveId>> CancelAllBookingsRelatedToEquipment(string equipmentId)
    {
        var bookingEntities = await _unitOfWork.BookingRepository.FindAllAsync(b => b.EquipmentId == equipmentId);
        foreach (var booking in bookingEntities)
        {
            booking.Status = enBookingStatus.Cancelled;
        }
        await _unitOfWork.BookingRepository.UpdateRangeAsync(bookingEntities);
        await _unitOfWork.SaveChangesAsync();

        return Response<IEntityHaveId>.Success();
    }
}
