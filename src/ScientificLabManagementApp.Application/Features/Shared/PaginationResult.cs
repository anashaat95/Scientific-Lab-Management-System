namespace ScientificLabManagementApp.Application;

public class PaginationResult<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class
{
    private IMapper _mapper { get; set; }
    public PaginationResult(PagedList<TEntity> sourceList, AllResourceParameters parameters)
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _mapper = serviceProvider!.GetRequiredService<IMapper>();
        Items = _mapper.Map<IEnumerable<TDto>>(sourceList);

        Meta = new PaginationMeta
        {
            CurrentPage = sourceList.CurrentPage,
            PageSize = sourceList.PageSize,
            TotalCount = sourceList.TotalCount,
            TotalPages = sourceList.TotalPages,

            previousPageLink = sourceList.HasPrevious ?
            ApiUrlFactory<TEntity>.CreatePreviousOrNextPageLink(parameters, eUriResourceType.PreviousPage) : null,

            nextPageLink = sourceList.HasNext ?
            ApiUrlFactory<TEntity>.CreatePreviousOrNextPageLink(parameters, eUriResourceType.NextPage) : null
        };
    }

    public IEnumerable<TDto> Items { get; private set; }

    public PaginationMeta Meta { get; private set; }
}


public class PaginationMeta
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public string? previousPageLink { get; set; }
    public string? nextPageLink { get; set; }
}