namespace ScientificLabManagementApp.Infrastructure;

internal static class QueryExtensions
{
    public static IQueryable<T> ApplyIncludes<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
    where T : class
    {
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }
        return query;
    }
}
