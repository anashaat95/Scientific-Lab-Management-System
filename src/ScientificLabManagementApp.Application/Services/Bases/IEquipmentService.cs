namespace ScientificLabManagementApp.Application;
public interface IEquipmentService
{
    Task<Response<IEntityHaveId>> UpdateEquipmentIfBookingConfirmed(Equipment equipment);
    Task<Response<IEntityHaveId>> UpdateEquipmentIfBookingDeleted(Equipment equipment);
    Task<Response<IEntityHaveId>> UpdateEquipmentBasedOnMaintenaceStatus(Equipment equipment, enMaintenanceStatus maintenanceStatus);
    Task<Response<IEntityHaveId>> UpdateEquipmentIfMaintenaceLogDeleted(Equipment equipment);
    Task<Response<IEntityHaveId>> CancelAllBookingsRelatedToEquipment(string equipmentId);
}
