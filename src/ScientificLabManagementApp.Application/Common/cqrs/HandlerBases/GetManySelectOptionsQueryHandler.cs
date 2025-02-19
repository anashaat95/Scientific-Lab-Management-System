namespace ScientificLabManagementApp.Application;

public class GetManySelectOptionsQueryHandler<TRequest, TEntity> : GetManyQueryHandlerBase<TRequest, TEntity, SelectOptionDto>
    where TRequest : IRequest<Response<IEnumerable<SelectOptionDto>>>
    where TEntity : class, IEntityBase
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _basicService.GetSelectOptionsAsync(e => true);
    }
}