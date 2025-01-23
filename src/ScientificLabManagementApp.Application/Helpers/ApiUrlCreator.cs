namespace ScientificLabManagementApp.Application;

internal static class ApiUrlFactory<TEntity> where TEntity : IEntityBase
{
    public static string? Create(string? id)
    {
        if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id))
            return null;

        var entityName = typeof(TEntity).Name;

        if (entityName == nameof(ApplicationUser))
            return $"/api/User/{id}";

        return $"/api/{entityName}/{id}";
    }
}
