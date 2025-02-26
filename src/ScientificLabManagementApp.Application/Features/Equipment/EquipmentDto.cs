namespace ScientificLabManagementApp.Application;
public class EquipmentDto : IEntityHaveId
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int total_quantity { get; set; }
    public int reserved_quantity { get; set; }
    public string Type { get; set; }
    public string Status { get; set; }
    public DateTime purchase_date { get; set; }
    public string? serial_number { get; set; }
    public string? Specifications { get; set; }
    public string? Description { get; set; }
    public string can_be_left_overnight { get; set; }
    public string? image_url { get; set; }
    public string company_url { get; set; }
    public string company_name { get; set; }
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}


public class EquipmentWithBookingsDto : EquipmentDto
{
    public List<BookingDto>? Bookings { get; set; }
}