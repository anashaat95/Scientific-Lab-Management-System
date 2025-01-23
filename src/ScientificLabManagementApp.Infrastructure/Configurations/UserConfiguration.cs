namespace ScientificLabManagementApp.Infrastructure;

public class UserConfiguration : EntityBaseConfiguration<ApplicationUser>
{
    public override void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.FirstName).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");
        builder.Property(x => x.LastName).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");

        builder.Property(x => x.ImageUrl).HasColumnType($"nvarchar({StringColumnLimits.URL})");
        builder.Property(x => x.GoogleScholarUrl).HasColumnType($"nvarchar({StringColumnLimits.URL})");
        builder.Property(x => x.AcademiaUrl).HasColumnType($"nvarchar({StringColumnLimits.URL})");
        builder.Property(x => x.ScopusUrl).HasColumnType($"nvarchar({StringColumnLimits.URL})");
        builder.Property(x => x.ResearcherGateUrl).HasColumnType($"nvarchar({StringColumnLimits.URL})");
        builder.Property(x => x.ExpertiseArea).HasColumnType($"nvarchar({StringColumnLimits.NAME})");

        // Navigation properties and relationships
        builder.HasOne(u => u.Company).WithMany(c => c.Users)
            .HasForeignKey(u => u.CompanyId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(u => u.Department).WithMany(r => r.Users)
            .HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(u => u.Lab).WithOne(l => l.Supervisior)
            .HasForeignKey<Lab>(l => l.SupervisiorId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Bookings).WithOne(b => b.User)
            .HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(u => u.MaintenanceLogs).WithOne(m => m.Technician)
            .HasForeignKey(u => u.TechnicianId).OnDelete(DeleteBehavior.Restrict);
    }
}
