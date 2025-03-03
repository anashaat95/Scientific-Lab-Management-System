﻿namespace ScientificLabManagementApp.Application;
public static class IdentityResultExtensions
{
    public static string ConvertErrorsToString(this IdentityResult Result)
    {
        string Value = "";
        foreach (var error in Result.Errors)
        {
            Value += "- " + error.Description + "; \n";
        }
        return Value;
    }
}
