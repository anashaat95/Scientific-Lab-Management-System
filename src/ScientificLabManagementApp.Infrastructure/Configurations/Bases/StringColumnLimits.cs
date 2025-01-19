namespace ScientificLabManagementApp.Infrastructure;

public static class StringColumnLimits
{
    public static int NAME { get; } = 50;
    public static int ADDRESS { get; } = 500;
    public static int ZIP_CODE { get; } = 50;
    public static int SERIAL_NUMBER { get; } = 300;
    public static int DESCRIPTION { get; } = 4000;
    public static int URL { get; } = 4000;
    public static string MAX { get; } = "max";
    public static int MAX_IN_NUMBER { get; } = 4000;
}
