namespace ScientificLabManagementApp.Domain;

public class Lab : EntityBase
{
    public string Name { get; set; }
    public int Capacity { get; set; }
    public TimeOnly OpeningTime { get; set; }
    public TimeOnly ClosingTime { get; set; }
    public string SupervisiorId { get; set; }
    public virtual ApplicationUser Supervisior { get; set; } = null!;

    public string DepartmentId { get; set; }
    public virtual Department Department { get; set; } = null!;
}
