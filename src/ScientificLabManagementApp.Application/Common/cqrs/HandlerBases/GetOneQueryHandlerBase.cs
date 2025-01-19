namespace ScientificLabManagementApp.Application;

public class GetOneQueryHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : IRequest<Response<TDto>>, IEntityHaveId
    where TEntity : class
    where TDto : class
{
    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityDto = await _basicService.GetDtoByIdAsync(request.Id);
        return entityDto is not null ? Ok200(entityDto) : NotFound<TDto>($"No resource found with Id = {request.Id}");
    }
}

