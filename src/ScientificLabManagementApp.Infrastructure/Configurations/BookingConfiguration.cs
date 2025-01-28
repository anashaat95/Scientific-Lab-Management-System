namespace ScientificLabManagementApp.Infrastructure;

public class BookingConfiguration : EntityBaseConfiguration<Booking>
{
    public override void Configure(EntityTypeBuilder<Booking> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.StartDateTime).IsRequired().HasColumnType("datetime");
        builder.Property(x => x.EndDateTime).IsRequired().HasColumnType("datetime");

        builder.Property(x => x.IsOnOverNight).IsRequired().HasColumnType("bit");

        builder.Property(x => x.Notes).HasColumnType($"nvarchar({StringColumnLimits.MAX})");

        builder.Property(x => x.Status).IsRequired().HasEnumConversion().HasColumnType($"nvarchar({StringColumnLimits.MAX})");

        // Navigation properties and relationships
        builder.HasOne(x => x.Equipment).WithMany(e => e.Bookings).HasForeignKey(x => x.EquipmentId).OnDelete(DeleteBehavior.Restrict);
    }
}
