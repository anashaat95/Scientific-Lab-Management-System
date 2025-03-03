
using Azure.Core;
using MediatR;

namespace ScientificLabManagementApp.Application;
public class GetManyEquipmentHandler : GetManyQueryHandlerBase<GetManyEquipmentQuery, Equipment, EquipmentDto>
{
    protected override Task<PagedList<EquipmentDto>> GetEntityDtos(GetManyEquipmentQuery request)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        return _basicService.GetAllAsync(parameters, e => e.Company);
    }
}

public class GetManyEquipmentWithBookingsHandler : GetManyQueryHandlerBase<GetManyEquipmentWithBookingsQuery, Equipment, EquipmentWithBookingsDto>
{
    private readonly IEquipmentService _equipmentService;

    public GetManyEquipmentWithBookingsHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected override Task<PagedList<EquipmentWithBookingsDto>> GetEntityDtos(GetManyEquipmentWithBookingsQuery request)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        return _equipmentService.GetAllEquipmentsWithBookingsDtoByIdAsync(parameters);
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
public class AddEquipmentHandler : AddCommandHandlerBase<AddEquipmentCommand, Equipment, EquipmentDto>
{
    public override async Task<Response<EquipmentDto>> Handle(AddEquipmentCommand request, CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<Equipment>(request);
        if (request.Data.image != null) entityToAdd.ImageUrl = await _cloudinaryService.GetUrlOfUploadedImage(request.Data.image);
        var resultDto = await _basicService.AddAsync(entityToAdd);
        return Created(resultDto);
    }
}

public class UpdateEquipmentHandler : UpdateCommandHandlerBase<UpdateEquipmentCommand, Equipment, EquipmentDto>
{

    protected readonly IEquipmentService _equipmentService;

    public UpdateEquipmentHandler(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    protected async override Task<Response<EquipmentDto>> DoUpdate(UpdateEquipmentCommand updateRequest, Equipment equipmentToUpdate)
    {
        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();
        try
        {

            var updatedEntity = _mapper.Map(updateRequest, equipmentToUpdate);
            updatedEntity.ReservedQuantity = 0;

            if (updateRequest.Data.image != null) updatedEntity.ImageUrl = await _cloudinaryService.GetUrlOfUploadedImage(updateRequest.Data.image);

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