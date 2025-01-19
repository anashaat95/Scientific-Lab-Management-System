using Microsoft.AspNetCore.Http;

namespace ScientificLabManagementApp.Application;
public static class ValidationLimitsConfig
{
    public const string URL_PATTERN = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
    public const string EMAIL_REGEX_PATTERN = @"^(?!.*\.\.)[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+\.[a-zA-Z]{2,}$";

    public static class NAME
    {
        public static int MIN { get; } = 4;
        public static int MAX { get; } = StringColumnLimits.NAME;
    }

    public static class ADDRESS
    {
        public static int MIN { get; } = 4;
        public static int MAX { get; } = StringColumnLimits.ADDRESS;
    }

    public static class ZIP_CODE
    {
        public static int MIN { get; } = 4;
        public static int MAX { get; } = StringColumnLimits.ZIP_CODE;
    }
    public static class SERIAL_NUMBER
    {
        public static int MIN { get; } = 4;
        public static int MAX { get; } = StringColumnLimits.SERIAL_NUMBER;
    }

    public static class DESCRIPTION
    {
        public static int MIN { get; } = 4;
        public static int MAX { get; } = StringColumnLimits.DESCRIPTION;
    }

    public static class URL
    {
        public static int MIN { get; } = 4;
        public static int MAX { get; } = StringColumnLimits.URL;
    }

    public static int MIN { get; } = 4;
    public static int MAX { get; } = StringColumnLimits.MAX_IN_NUMBER;
}
