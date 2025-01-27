using Azure.Core;

namespace ScientificLabManagementApp.Application;

public class GetOneQueryHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : IRequest<Response<TDto>>, IEntityHaveId
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityDto = await GetEntityDto(request);
        return entityDto is not null ? Ok200(entityDto) : NotFound<TDto>($"No resource found with Id = {request.Id}");
    }

    protected virtual Task<TDto?> GetEntityDto(TRequest request)
    {
        return _basicService.GetDtoByIdAsync(request.Id);
    }
}

