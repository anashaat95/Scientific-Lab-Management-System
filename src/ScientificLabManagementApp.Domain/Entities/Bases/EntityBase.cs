namespace ScientificLabManagementApp.Domain;

public abstract class EntityBase : IEntityBase
{
    public string Id {get; set;} = Guid.NewGuid().ToString();
    public DateTime CreatedAt{get; set;}
    public DateTime? UpdatedAt { get; set;}
}
