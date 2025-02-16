using Azure.Core;

namespace ScientificLabManagementApp.Application;
public class GetManyBookingHandler : GetManyQueryHandlerBase<GetManyBookingQuery, Booking, BookingDto>
{
    protected override Task<IEnumerable<BookingDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.Equipment, e => e.User);
    }
}

public class GetOneBookingByIdHandler : GetOneQueryHandlerBase<GetOneBookingByIdQuery, Booking, BookingDto>
{
    protected override Task<BookingDto?> GetEntityDto(GetOneBookingByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Equipment, e => e.User);
    }
}
public class AddBookingHandler : AddCommandHandlerBase<AddBookingCommand, Booking, BookingDto>
{
    protected readonly IBaseService<Equipment, EquipmentDto> _equipmentService;

    public AddBookingHandler(IBaseService<Equipment, EquipmentDto> equipmentService)
    {
        _equipmentService = equipmentService;
    }

    public async override Task<Response<BookingDto>> Handle(AddBookingCommand request, CancellationToken cancellationToken)
    {
        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();

        try
        {
            // Map and add booking entity
            var entityToAdd = _mapper.Map<Booking>(request);
            var resultEntity = await _unitOfWork.BookingRepository.AddAsync(entityToAdd);
            await _unitOfWork.SaveChangesAsync();

            // Update Equipment if booking is confirmed
            if (request.Data.Status == enBookingStatus.Confirmed)
            {
                var equipment = await _unitOfWork.EquipmentRepository.GetOneByIdAsync(request.Data.equipment_id);
                if (equipment == null)
                    return NotFound<BookingDto>("Equipment not found");

                // Increase Reserved Quantity
                equipment.ReservedQuantity += 1;

                // Check if Fully Booked
                if (equipment.ReservedQuantity == equipment.TotalQuantity)
                {
                    equipment.Status = enEquipmentStatus.FullyBooked;
                }
                else if (equipment.ReservedQuantity > equipment.TotalQuantity || equipment.Status == enEquipmentStatus.FullyBooked)
                {
                    return BadRequest<BookingDto>("Booking can not be added because this equipment is fully booked");
                }

                await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            // Return Created Response
            var resultDto = _mapper.Map<BookingDto>(entityToAdd);
            return Created(resultDto);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<BookingDto>($"An error occurred: {ex.Message}");
        }
    }
}
public class UpdateBookingHandler : UpdateCommandHandlerBase<UpdateBookingCommand, Booking, BookingDto>
{
    protected override async Task<Response<BookingDto>> DoUpdate(UpdateBookingCommand updateRequest, Booking entityToUpdate)
    {

        if (entityToUpdate.Status == updateRequest.Data.Status) return Updated(_mapper.Map<BookingDto>(entityToUpdate));

        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();

        try
        {
            var updatedBooking = _mapper.Map(updateRequest, entityToUpdate);
            var resultEntity = await _unitOfWork.BookingRepository.UpdateAsync(updatedBooking);
            await _unitOfWork.SaveChangesAsync();


            // Update Equipment if booking is confirmed
            if (updateRequest.Data.Status == enBookingStatus.Confirmed)
            {
                var equipment = await _unitOfWork.EquipmentRepository.GetOneByIdAsync(updateRequest.Data.equipment_id);
                if (equipment == null)
                    return NotFound<BookingDto>("Equipment not found");

                // Increase Reserved Quantity
                equipment.ReservedQuantity += 1;

                // Check if Fully Booked
                if (equipment.ReservedQuantity == equipment.TotalQuantity)
                {
                    equipment.Status = enEquipmentStatus.FullyBooked;
                }
                else if (equipment.ReservedQuantity > equipment.TotalQuantity || equipment.Status == enEquipmentStatus.FullyBooked)
                {
                    return BadRequest<BookingDto>("Booking can not be added because this equipment is fully booked");
                }

                await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            // Return Created Response
            var resultDto = _mapper.Map<BookingDto>(resultEntity);
            return Created(resultDto);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<BookingDto>($"An error occurred: {ex.Message}");
        }
    }

}
public class DeleteBookingHandler : DeleteCommandHandlerBase<DeleteBookingCommand, Booking, BookingDto>
{
    protected async override Task<Response<BookingDto>> DoDelete(Booking bookingToDelete)
    {
        using var transaction = _unitOfWork;
        await transaction.BeginTransactionAsync();

        try
        {
            await _unitOfWork.BookingRepository.DeleteAsync(bookingToDelete);
            await _unitOfWork.SaveChangesAsync();

            // Get Equipment
            var equipment = await _unitOfWork.EquipmentRepository.GetOneByIdAsync(bookingToDelete.EquipmentId);
            if (equipment == null)
                return NotFound<BookingDto>("Equipment not found");

            equipment.Status = enEquipmentStatus.Available;
            equipment.ReservedQuantity -= 1;


            if (equipment.ReservedQuantity < 0)
            {
                equipment.ReservedQuantity = 0;
            }

            await _unitOfWork.EquipmentRepository.UpdateAsync(equipment);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return Deleted<BookingDto>();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return BadRequest<BookingDto>($"An error occurred: {ex.Message}");
        }
    }
}
