using Microsoft.EntityFrameworkCore;

namespace ScientificLabManagementApp.Domain;

public class PagedList<TEntity> : List<TEntity>
    where TEntity : class
{
    public int CurrentPage { get; private set; } = 1;
    public int TotalPages { get; private set; } = 1;
    public int PageSize { get; set; } = 1;
    public int TotalCount { get; set; } = 1;
    public bool HasPrevious => (CurrentPage > 1);
    public bool HasNext => CurrentPage < TotalPages;

    protected PagedList(List<TEntity> items, int pageNumber,int pageSize, int count)
    {
        TotalCount = count;
        TotalPages = (int)Math.Ceiling((double)count / pageSize);
        CurrentPage = pageNumber ;
        PageSize = pageSize;
        AddRange(items);
    }

    public PagedList()
    {
        
    }
    public static async Task<PagedList<TEntity>> CreateAsync(IQueryable<TEntity> source, int pageNumber, int pageSize)
    {
        var count = source.Count();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<TEntity>(items, pageNumber, pageSize, count);
    }
}
