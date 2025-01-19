namespace ScientificLabManagementApp.Infrastructure;

public class CountryConfiguration : EntityBaseConfiguration<Country>
{
    public override void Configure(EntityTypeBuilder<Country> builder)
    {
        base.Configure(builder);
        builder.ToTable("Countries");
    }
}
