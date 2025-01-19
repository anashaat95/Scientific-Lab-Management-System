namespace ScientificLabManagementApp.Infrastructure;

public class LabConfiguration : EntityBaseConfiguration<Lab>
{
    public override void Configure(EntityTypeBuilder<Lab> builder)
    {
        base.Configure(builder);
        builder.Property(x => x.Name).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");

        builder.Property(x => x.Capacity).IsRequired().HasColumnType("int");

        builder.Property(x => x.OpeningTime).IsRequired().HasColumnType("time");
        builder.Property(x => x.ClosingTime).IsRequired().HasColumnType("time");

        // Relationships
        builder.HasOne(l => l.Department).WithMany(m => m.Labs).HasForeignKey(l => l.DepartmentId).OnDelete(DeleteBehavior.Restrict);
    }
}
