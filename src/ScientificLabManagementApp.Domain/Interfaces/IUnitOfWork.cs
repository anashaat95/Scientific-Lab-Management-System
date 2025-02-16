namespace ScientificLabManagementApp.Domain;
public interface IUnitOfWork :IDisposable
{
    IGenericRepository<Equipment> EquipmentRepository { get; }
    IGenericRepository<Booking> BookingRepository { get; }
    IGenericRepository<MaintenanceLog> MaintenanceLogRepository { get; }


    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    void Dispose();
}
