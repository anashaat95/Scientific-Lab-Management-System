namespace ScientificLabManagementApp.Application;

public class BookingCommandData
{
    public DateTime start_date_time { get; set; }
    public DateTime end_date_time { get; set; }
    public bool is_on_overnight { get; set; } = false;
    public string? Notes { get; set; }
    public enBookingStatus Status { get; set; } = enBookingStatus.Confirmed;
    public string user_id { get; set; }
    public string equipment_id { get; set; }
    public string? sub_equipment_id { get; set; }
}

public class AddBookingCommand : AddCommandBase<BookingDto, BookingCommandData> { }

public class UpdateBookingCommand : UpdateCommandBase<BookingDto, BookingCommandData> { }

public class DeleteBookingCommand : DeleteCommandBase<BookingDto> { }


