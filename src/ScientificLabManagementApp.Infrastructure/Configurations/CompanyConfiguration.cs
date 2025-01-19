namespace ScientificLabManagementApp.Infrastructure;

public class CompanyConfiguration : EntityBaseConfiguration<Company>
{
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");

        builder.Property(c => c.Street).HasColumnType($"nvarchar({StringColumnLimits.ADDRESS})");
        builder.Property(c => c.ZipCode).HasColumnType($"nvarchar({StringColumnLimits.ZIP_CODE})");

        builder.Property(c => c.Website).HasColumnType($"nvarchar({StringColumnLimits.URL})");

        // Navigation properties and relationships
        builder.HasOne(c => c.City).WithMany(c => c.Companies).HasForeignKey(c => c.CityId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Country).WithMany(c => c.Companies).HasForeignKey(c => c.CountryId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Departments).WithOne(d => d.Company).HasForeignKey(d => d.CompanyId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Equipments).WithOne(e => e.Company).HasForeignKey(e => e.CompanyId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.Departments).WithOne(d => d.Company).HasForeignKey(c => c.CompanyId).OnDelete(DeleteBehavior.Restrict);
    }
}
