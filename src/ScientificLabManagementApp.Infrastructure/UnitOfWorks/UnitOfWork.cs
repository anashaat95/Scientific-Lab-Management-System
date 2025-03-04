
using Microsoft.EntityFrameworkCore.Storage;

namespace ScientificLabManagementApp.Infrastructure;

public class UnitOfWork : IUnitOfWork
{

    private IDbContextTransaction? _transaction;
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context, IGenericRepository<Equipment> equipmentRepository, IGenericRepository<Booking> bookingRepository, IGenericRepository<MaintenanceLog> maintenanceLogRepository)
    {
        _context = context;
        EquipmentRepository = equipmentRepository;
        BookingRepository = bookingRepository;
        MaintenanceLogRepository = maintenanceLogRepository;
    }

    public IGenericRepository<Equipment> EquipmentRepository { get; }
    public IGenericRepository<Booking> BookingRepository { get; }
    public IGenericRepository<MaintenanceLog> MaintenanceLogRepository { get; }


    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction == null) throw new InvalidOperationException("No active transaction");
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
        await _transaction.DisposeAsync();
        
        _transaction = null;
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
