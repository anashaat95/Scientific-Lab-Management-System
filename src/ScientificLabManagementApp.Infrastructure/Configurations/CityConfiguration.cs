namespace ScientificLabManagementApp.Infrastructure;

public class CityConfiguration : EntityBaseConfiguration<City>
{
    public override void Configure(EntityTypeBuilder<City> builder)
    {
        base.Configure(builder);

        builder.Property(c => c.Name).IsRequired().HasColumnType($"nvarchar({StringColumnLimits.NAME})");
    }
}
