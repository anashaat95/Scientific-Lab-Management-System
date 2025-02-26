namespace ScientificLabManagementApp.Application;
public class BookingDto : IEntityHaveId
{


    public string Id { get; set; } = String.Empty;
    public DateTime start_date_time { get; set; }
    public DateTime end_date_time { get; set; }
    public bool is_on_overnight { get; set; }
    public string Notes { get; set; } = String.Empty;
    public string Status { get; set; } = String.Empty;
    public string? user_url { get; set; } = String.Empty;
    public string user_name { get; set; } = String.Empty;
    public string? equipment_url { get; set; } = String.Empty;
    public string equipment_name { get; set; } = String.Empty;
    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}

