namespace ScientificLabManagementApp.Domain;

public class Booking : EntityBase, IEntityAddedByUser
{
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public bool IsOnOverNight { get; set; }
    public string Notes { get; set; }
    public enBookingStatus Status { get; set; }

    public string UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;

    public string EquipmentId { get; set; }
    public virtual Equipment Equipment { get; set; } = null!;
}
