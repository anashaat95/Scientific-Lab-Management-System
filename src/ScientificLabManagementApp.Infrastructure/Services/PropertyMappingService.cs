namespace ScientificLabManagementApp.Infrastructure;

public class PropertyMappingService<TSource, TDestination> : IPropertyMappingService<TSource, TDestination>
    where TSource : class
    where TDestination : class, IEntityBase
{
    private readonly Dictionary<string, PropertyMappingValue> _entityPropertyMapping
        = new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
        {
            {"Id", new PropertyMappingValue(new [] {"Id"}) },
            {"Name", new PropertyMappingValue(new [] {"Name"}) },
            {"CreatedAt", new PropertyMappingValue(new [] {"CreatedAt"}) },
            {"UpdatedAt", new PropertyMappingValue(new [] {"UpdatedAt"}) }
        };

    private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

    public PropertyMappingService()
    {
        _propertyMappings.Add(new PropertyMapping<TSource, TDestination>(_entityPropertyMapping));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping()
    {
        var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSource, TDestination>>();
        if (matchingMapping.Count() == 1)
        {
            return matchingMapping.First().MappingDictionary;
        }

        throw new Exception($"Cannot find property mapping instance " +
            $"for <{typeof(TSource)},{typeof(TDestination)}>");
    }

    public bool ValidateMappingExistsFor(string fields)
    {
        var mappingDictionary = GetPropertyMapping();
        if (string.IsNullOrWhiteSpace(fields)) return true;

        var fieldsAfterSplit = fields.Split(",");

        foreach (var field in fieldsAfterSplit)
        {
            var trimmedField = field.Trim();
            var indexOfFirstSpace = trimmedField.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

            if (!mappingDictionary.ContainsKey(propertyName))
                return false;
        }
        return true;
    }
}