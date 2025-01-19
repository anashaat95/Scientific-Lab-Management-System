namespace ScientificLabManagementApp.Infrastructure;

internal static class ConverterExtensions
{
    public static PropertyBuilder<T> HasEnumConversion<T>(this PropertyBuilder<T> Property)
        where T : struct, Enum
    {
        return Property.HasConversion(v => v.ToString(), v => (T)Enum.Parse(typeof(T), v!));
    }
}
