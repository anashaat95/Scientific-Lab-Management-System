namespace ScientificLabManagementApp.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }


    public DbSet<Lab> Labs { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<MaintenanceLog> MaintenanceLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EquipmentConfiguration).Assembly);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries<IEntityBase>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = DateTime.UtcNow;
            }
        }

        try
        {
            return base.SaveChanges();
        }
        catch 
        {
            return 0;
        }
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.SaveChanges();

        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (Exception)
        {
            return 0;
        }
    }
}
