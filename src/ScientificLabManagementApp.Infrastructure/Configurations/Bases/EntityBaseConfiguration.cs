namespace ScientificLabManagementApp.Infrastructure;

public class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntityBase
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd().HasDefaultValueSql("NEWID()");

        builder.Property(e => e.CreatedAt).IsRequired(true);
        builder.Property(e => e.UpdatedAt).IsRequired(false);
    }
}
