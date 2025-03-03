namespace ScientificLabManagementApp.Domain;

public static class ApiUrlFactory<TEntity> where TEntity : IEntityBase
{
    public static string? Create(string? id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            return null;

        var entityName = typeof(TEntity).Name;

        if (entityName == nameof(ApplicationUser) || entityName == nameof(MappingApplicationUser))
            return $"/api/User/{id}";

        return $"/api/{entityName}/{id}";
    }

    public static string? CreatePreviousOrNextPageLink(AllResourceParameters parameters, eUriResourceType type)
    {
        var entityName = typeof(TEntity).Name;
        var url = $"api/{entityName}?";

        if (parameters.Filter != null)
            url += $"filter={parameters.Filter}&";

        if (parameters.SearchQuery != null)
            url += $"searchQuery={parameters.SearchQuery}&";

        if (parameters.SortBy != null)
            url += $"sortBy={parameters.SortBy}&";

        if (parameters.Descending)
            url += $"Descending={parameters.Descending}&";

        switch (type)
        {
            case eUriResourceType.PreviousPage:
                url += $"pageNumber={parameters.PageNumber - 1}&pageSize={parameters.PageSize}";
                break;
            case eUriResourceType.NextPage:
                url += $"pageNumber={parameters.PageNumber + 1}&pageSize={parameters.PageSize}";
                break;
            default:
                url += $"pageNumber={parameters.PageNumber}&pageSize={parameters.PageSize}";
                break;
        }

        return url;
    }
}