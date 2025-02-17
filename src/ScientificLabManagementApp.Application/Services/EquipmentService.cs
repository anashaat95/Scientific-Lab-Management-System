namespace ScientificLabManagementApp.Application;

public class EquipmentService : IEquipmentService
{
    protected readonly IUnitOfWork  _unitOfWork;

    public EquipmentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<IEntityHaveId>> UpdateEquipmentIfBookingConfirmed(Equipment equipment)
    {
        // Increase Reserved Quantity
        equipment.ReservedQuantity += 1;

        // Check if Fully Booked
        if (equipment.ReservedQuantity == equipment.TotalQuantity)
        {
            equipment.Status = enEquipmentStatus.FullyBooked;
        }
        else if (equipment.ReservedQuantity > equipment.TotalQuantity || equipment.Status == enEquipmentStatus.FullyBooked)
        {
            return Response<IEntityHaveId>.Fail("Booking can not be added because this equipment is fully booked", HttpStatusCode.BadRequest);
        }

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
