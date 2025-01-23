namespace ScientificLabManagementApp.Application;
public class BookingDto : IEntityHaveId
{
    public string Id { get; set; }
    public string start_date_time { get; set; }
    public string end_date_time { get; set; }
    public bool is_on_overnight { get; set; }
    public string Notes { get; set; }
    public string Status { get; set; }
    public string user_url { get; set; }
    public string user_name { get; set; }
    public string equipment_url { get; set; }
    public string equipment_name { get; set; }
    public string sub_equipment_url { get; set; }
    public string sub_equipment_name { get; set; }

    public DateTime created_at { get; set; }
    public DateTime? updated_at { get; set; }
}

