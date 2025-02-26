
namespace ScientificLabManagementApp.Application;
public class GetManyEquipmentHandler : GetManyQueryHandlerBase<GetManyEquipmentQuery, Equipment, EquipmentDto>
{
    protected override Task<IEnumerable<EquipmentDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.Company);
    }
}

public class GetManyEquipmentWithBookingsHandler : GetManyQueryHandlerBase<GetManyEquipmentWithBookingsQuery, Equipment, EquipmentWithBookingsDto>
{
    private readonly IEquipmentService _equipmentService;

    public GetManyEquipmentWithBookingsHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }
    protected override Task<IEnumerable<EquipmentWithBookingsDto>> GetEntityDtos()
    {
        return _equipmentService.GetAllEquipmentsWithBookingsDtoByIdAsync();
    }
}




public class GetManyEquipmentSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyEquipmentSelectOptionsQuery, Equipment> { }
public class GetOneEquipmentByIdHandler : GetOneQueryHandlerBase<GetOneEquipmentByIdQuery, Equipment, EquipmentDto>
{
    protected override Task<EquipmentDto?> GetEntityDto(GetOneEquipmentByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Company);
    }
}

public class GetBookingsForEquipmentByEquipmentIdHandler : GetOneQueryHandlerBase<GetOneEquipmentWithBookingsByIdQuery, Equipment, EquipmentWithBookingsDto>
{
    private readonly IEquipmentService _equipmentService;

    public GetBookingsForEquipmentByEquipmentIdHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected override Task<EquipmentWithBookingsDto?> GetEntityDto(GetOneEquipmentWithBookingsByIdQuery request)
    {
        return _equipmentService.GetEquipmentWithBookingsDtoByIdAsync(request.Id);
    }
}
public class AddEquipmentHandler : AddCommandHandlerBase<AddEquipmentCommand, Equipment, EquipmentDto> { }

public class UpdateEquipmentHandler : UpdateCommandHandlerBase<UpdateEquipmentCommand, Equipment, EquipmentDto>
{

    protected readonly IEquipmentService _equipmentService;

    public UpdateEquipmentHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected async override Task<Response<EquipmentDto>> DoUpdate(UpdateEquipmentCommand updateRequest, Equipment equipmentToUpdate)
    {
        //if (updateRequest.Data.Status == enEquipmentStatus.FullyBooked || updateRequest.Data.Status == enEquipmentStatus.InMaintenance
        //    || equipmentToUpdate.Status == updateRequest.Data.Status)
        //    return Updated(_mapper.Map<EquipmentDto>(equipmentToUpdate));

        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();
        try
        {

            var updatedEntity = _mapper.Map(updateRequest, equipmentToUpdate);
            updatedEntity.ReservedQuantity = 0;
            await _unitOfWork.EquipmentRepository.UpdateAsync(updatedEntity);

            // Find and cancel related booking entities
            await _equipmentService.CancelAllBookingsRelatedToEquipment(updatedEntity.Id);

            var resultDto = _mapper.Map<EquipmentDto>(updatedEntity);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return Updated(_mapper.Map<EquipmentDto>(equipmentToUpdate));
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<EquipmentDto>($"An error occurred: {ex.Message}");
        }
    }
}

public class DeleteEquipmentHandler : DeleteCommandHandlerBase<DeleteEquipmentCommand, Equipment, EquipmentDto> { }