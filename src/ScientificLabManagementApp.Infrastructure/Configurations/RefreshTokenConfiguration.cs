namespace ScientificLabManagementApp.Infrastructure;

public class RefreshTokenConfiguration : EntityBaseConfiguration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder.ToTable("RefreshTokens");

        builder.Property(x => x.Token).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.Property(x => x.ExpiresIn).IsRequired().HasColumnType("datetime");
        builder.Property(x => x.RevokedAt).HasColumnType("datetime");

    }
}
