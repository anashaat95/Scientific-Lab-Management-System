
namespace ScientificLabManagementApp.Application;

public class GetManyEquipmentQuery : GetManyQueryBase<EquipmentDto>{}
public class GetManyEquipmentWithBookingsQuery : GetManyQueryBase<EquipmentWithBookingsDto> {}
public class GetManyEquipmentSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneEquipmentByIdQuery : GetOneQueryBase<EquipmentDto>{}
public class GetOneEquipmentWithBookingsByIdQuery : GetOneQueryBase<EquipmentWithBookingsDto>{}
