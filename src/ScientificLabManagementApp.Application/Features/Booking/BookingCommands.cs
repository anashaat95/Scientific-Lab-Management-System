namespace ScientificLabManagementApp.Application;

public abstract class BookingCommandData
{
    public DateTime start_date_time { get; set; }
    public DateTime end_date_time { get; set; }
    public bool is_on_overnight { get; set; } = false;
    public string? Notes { get; set; }
    public enBookingStatus Status { get; set; } = enBookingStatus.Confirmed;
    public string user_id { get; set; }
    public string equipment_id { get; set; }
}
public class AddBookingCommandData : BookingCommandData
{

}

public class UpdateBookingCommandData : BookingCommandData
{
}


public class AddBookingCommand : AddCommandBase<BookingDto, AddBookingCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.ResearcherLevel;
}

public class UpdateBookingCommand : UpdateCommandBase<BookingDto, UpdateBookingCommandData>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.ResearcherLevel;
}

public class DeleteBookingCommand : DeleteCommandBase<BookingDto>
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.ResearcherLevel;
}


