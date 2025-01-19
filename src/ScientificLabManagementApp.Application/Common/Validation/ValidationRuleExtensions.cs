namespace ScientificLabManagementApp.Application;

public static class ValidationRuleExtensions
{
    public static IRuleBuilderOptions<T, TProperty> ApplyNotEmptyRule<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotEmpty().WithMessage("Required! {PropertyName} cannot be empty");
    }

    public static IRuleBuilderOptions<T, TProperty> ApplyNotNullableRule<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.NotNull().WithMessage("Required! {PropertyName} cannot be null");
    }

    public static IRuleBuilderOptions<T, string> ApplyMinLengthRule<T>(this IRuleBuilder<T, string> ruleBuilder, int MinLength)
    {
        return ruleBuilder.MinimumLength(MinLength).WithMessage("{PropertyName}" + $" must be at least {MinLength} characters");
    }

    public static IRuleBuilderOptions<T, string> ApplyMaxLengthRule<T>(this IRuleBuilder<T, string> ruleBuilder, int MaxLength)
    {
        return ruleBuilder.MaximumLength(MaxLength).WithMessage("{PropertyName}" + $" must be less than or equal {MaxLength} characters");
    }

    public static IRuleBuilderOptions<T, string> ApplyMinMaxLengthRule<T>(this IRuleBuilder<T, string> ruleBuilder, int MinLength, int MaxLength)
    {
        return ruleBuilder.ApplyMinLengthRule(MinLength).ApplyMaxLengthRule(MaxLength);
    }

    public static IRuleBuilderOptions<T, string> ApplyValidUrlRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(ValidationLimitsConfig.URL_PATTERN).WithMessage("{PropertyName}" + "is not a valid URL.");
    }
    public static IRuleBuilderOptions<T, string> ApplyValidEmailRule<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Matches(ValidationLimitsConfig.EMAIL_REGEX_PATTERN).WithMessage("{PropertyName}" + "is not a valid email.");
    }

    public static IRuleBuilderOptions<T, string?> ValidateOptionalUrl<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        string urlPattern = ValidationLimitsConfig.URL_PATTERN)
    {
        return ruleBuilder
            .Must(url => string.IsNullOrWhiteSpace(url) || Regex.IsMatch(url, urlPattern))
            .WithMessage("The provided {PropertyName} is not a valid URL.");
    }
}
