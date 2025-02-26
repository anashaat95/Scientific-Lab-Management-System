
namespace ScientificLabManagementApp.Application;

public class GetManyEquipmentQuery : GetManyQueryBases<EquipmentDto>{}
public class GetManyEquipmentWithBookingsQuery : GetManyQueryBases<EquipmentWithBookingsDto> {}
public class GetManyEquipmentSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneEquipmentByIdQuery : GetOneQueryBase<EquipmentDto>{}
public class GetOneEquipmentWithBookingsByIdQuery : GetOneQueryBase<EquipmentWithBookingsDto>{}
