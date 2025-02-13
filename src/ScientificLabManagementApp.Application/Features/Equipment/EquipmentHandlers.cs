
namespace ScientificLabManagementApp.Application;
public class GetManyEquipmentHandler : GetManyQueryHandlerBase<GetManyEquipmentQuery, Equipment, EquipmentDto>
{
    protected override Task<IEnumerable<EquipmentDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.Company);
    }
}
public class GetOneEquipmentByIdHandler : GetOneQueryHandlerBase<GetOneEquipmentByIdQuery, Equipment, EquipmentDto>
{
    protected override Task<EquipmentDto?> GetEntityDto(GetOneEquipmentByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Company);
    }
}
public class AddEquipmentHandler : AddCommandHandlerBase<AddEquipmentCommand, Equipment, EquipmentDto> { }

public class DeleteEquipmentHandler : DeleteCommandHandlerBase<DeleteEquipmentCommand, Equipment, EquipmentDto> { }

public class UpdateEquipmentHandler : UpdateCommandHandlerBase<UpdateEquipmentCommand, Equipment, EquipmentDto> { }
