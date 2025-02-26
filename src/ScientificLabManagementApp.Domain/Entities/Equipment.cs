namespace ScientificLabManagementApp.Domain;

public class Equipment : EntityBase
{
    // General Properties
    public string Name { get; set; }
    public int TotalQuantity { get; set; }
    public int ReservedQuantity { get; set; } = 0;
    public enEquipmentType Type { get; set; }
    public enEquipmentStatus Status { get; set; } = enEquipmentStatus.Available;
    public DateTime PurchaseDate { get; set; }
    public string? SerialNumber { get; set; }
    public string? Specifications { get; set; }
    public string? Description { get; set; }
    public bool CanBeLeftOverNight { get; set; } = false;
    public string? ImageUrl { get; set; }

    // Relationships
    public virtual List<Booking> Bookings { get; set; } = new ();
    public virtual List<MaintenanceLog> MaintenanceLogs { get; set; } = new ();

    // Organizational Context
    public string CompanyId { get; set; }
    public virtual Company Company { get; set; } = null!;
}
