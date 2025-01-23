namespace ScientificLabManagementApp.Application;

public class BookingCommandData
{
    public DateTime start_date_time { get; set; }
    public DateTime end_date_time { get; set; }
    public bool is_on_overnight { get; set; } = false;
    public string? Notes { get; set; }
    public enBookingStatus Status { get; set; } = enBookingStatus.Confirmed;
}
public class AddBookingCommandData : BookingCommandData
{
    public string user_id { get; set; }
    public string equipment_id { get; set; }
    public string? sub_equipment_id { get; set; }
}


public class AddBookingCommand : AddCommandBase<BookingDto, BookingCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.ResearcherLevel;
    public new AddBookingCommandData Data { get; set; }
}

public class UpdateBookingCommand : UpdateCommandBase<BookingDto, BookingCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.ResearcherLevel;
}

public class DeleteBookingCommand : DeleteCommandBase<BookingDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.ResearcherLevel;
}


