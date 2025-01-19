namespace ScientificLabManagementApp.Application;

public class AddCommandHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : IRequest<Response<TDto>>
    where TEntity : class
    where TDto : class
{
    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<TEntity>(request);
        var resultDto = await _basicService.AddAsync(entityToAdd);
        return Created(resultDto);
    }
}
