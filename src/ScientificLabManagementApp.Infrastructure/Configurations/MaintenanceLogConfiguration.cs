namespace ScientificLabManagementApp.Infrastructure;

public class MaintenanceLogConfiguration : EntityBaseConfiguration<MaintenanceLog>
{
    public override void Configure(EntityTypeBuilder<MaintenanceLog> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Description).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.DESCRIPTION})");
    }
}
