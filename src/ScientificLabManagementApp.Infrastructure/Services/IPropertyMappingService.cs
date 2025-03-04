
namespace ScientificLabManagementApp.Infrastructure;
public interface IPropertyMappingService<TSource, TDestination>
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping();
    bool ValidateMappingExistsFor(string fields);
}
