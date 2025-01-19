namespace ScientificLabManagementApp.Infrastructure;

public class DepartmentConfiguration : EntityBaseConfiguration<Department>
{
    public override void Configure(EntityTypeBuilder<Department> builder)
    {
        base.Configure(builder);
        builder.Property(c => c.Name).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");
        builder.Property(c => c.Location).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.ADDRESS})");
    }
}
