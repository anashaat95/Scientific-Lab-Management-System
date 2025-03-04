using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq.Dynamic.Core;

namespace ScientificLabManagementApp.Infrastructure;

internal static class QueryExtensions
{
    public static IQueryable<TEntity> ApplyIncludes<TEntity>(this IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
    where TEntity : class
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

    public static IQueryable<TEntity> ApplyFilter<TEntity>(this IQueryable<TEntity> query, string? Filter)
    {
        if (query is null) throw new ArgumentNullException(nameof(query));

        if (String.IsNullOrEmpty(Filter)) return query;

        var filterParts = Filter.Split(":");
        if (filterParts.Length != 2) return query;

        var propertyName = filterParts[0];
        var filterValue = filterParts[1];

        var Parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(Parameter, propertyName);
        var constant = Expression.Constant(filterValue);
        var equals = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, Parameter);

        return query.Where(lambda);
    }


    public static IQueryable<TEntity> ApplySort<TSource, TEntity>(this IQueryable<TEntity> query, string? orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        where TSource : class
        where TEntity : class, IEntityBase
    {

        if (query is null) throw new ArgumentNullException(nameof(query));
        if (mappingDictionary is null) throw new ArgumentNullException(nameof(mappingDictionary));
        if (String.IsNullOrEmpty(orderBy)) return query;

        var orderByString = String.Empty;
        var orderByAfterSplit = orderBy.Split(",");
        foreach (var orderByClause in orderByAfterSplit)
        {
            var trimmedOrderByClause = orderByClause.Trim();
            var orderDescending = trimmedOrderByClause.EndsWith(" desc");
            var indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ? trimmedOrderByClause : trimmedOrderByClause.Remove(indexOfFirstSpace);

            if (!mappingDictionary.ContainsKey(propertyName)) throw new ArgumentException($"Key mapping for {propertyName} is missing");
            
            var propertyMappingValue = mappingDictionary[propertyName];
            if (propertyMappingValue is null) throw new ArgumentNullException(nameof(propertyMappingValue));

            foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
            {
                if (propertyMappingValue.Revert)
                {
                    orderDescending = !orderDescending;
                }
                orderByString += destinationProperty + (orderDescending ? " descending," : " ascending,");
            }
            orderByString = orderByString.TrimEnd(',');
        }

        return query.OrderBy(orderByString);
    }
}
