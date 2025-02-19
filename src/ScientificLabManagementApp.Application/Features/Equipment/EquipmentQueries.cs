namespace ScientificLabManagementApp.Application;

public class GetManyEquipmentQuery : GetManyQueryBases<EquipmentDto>{}
public class GetManyEquipmentSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneEquipmentByIdQuery : GetOneQueryBase<EquipmentDto>{}
