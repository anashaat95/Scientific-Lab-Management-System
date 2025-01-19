namespace ScientificLabManagementApp.Infrastructure;

public class EquipmentConfiguration : EntityBaseConfiguration<Equipment>
{
    public override void Configure(EntityTypeBuilder<Equipment> builder)
    {
        base.Configure(builder);

        builder.ToTable("Equipments");

        builder.Property(e => e.Name).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");

        builder.Property(e => e.TotalQuantity).IsRequired().HasColumnType("int");
        builder.Property(e => e.ReservedQuantity).IsRequired().HasColumnType("int").HasDefaultValue(0);
        builder.Property(e => e.Type).HasEnumConversion();
        builder.Property(e => e.Status).HasEnumConversion().IsRequired();
        builder.Property(e => e.PurchaseDate).IsRequired().HasColumnType("datetime");
        builder.Property(e => e.SerialNumber).HasColumnType($"nvarchar({StringColumnLimits.SERIAL_NUMBER})");
        builder.Property(e => e.Specifications).HasColumnType($"nvarchar({StringColumnLimits.DESCRIPTION})");
        builder.Property(e => e.Description).HasColumnType($"nvarchar({StringColumnLimits.DESCRIPTION})");

        builder.Property(e => e.CanBeLeftOverNight).HasColumnType("bit").HasDefaultValue(false);
        builder.Property(e => e.ImageUrl).HasColumnType($"nvarchar({StringColumnLimits.URL})");


        // Navigation property and relationship
        builder.HasOne(e => e.SubEquipment).WithOne(s => s.ParentEquipment).HasForeignKey<Equipment>(e => e.ParentEquipmentId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.MaintenanceLogs).WithOne(m=>m.Equipment).HasForeignKey(m=>m.EquipmentId).OnDelete(DeleteBehavior.Restrict);
    }
}
