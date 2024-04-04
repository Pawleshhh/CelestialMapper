using CelestialMapper.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace CelestialMapper.UI;

public abstract class ValidationRule<T> : ValidationRule
{

    protected IResourceResolver ResourceResolver { get; }

    public string InvalidResultString { get; set; } = string.Empty;

    public ValidationRule()
    {
        ResourceResolver = App.ServiceProvider.GetService<IResourceResolver>()!;
    }

    public ValidationRule(IServiceProvider serviceProvider)
    {
        ResourceResolver = serviceProvider.GetService<IResourceResolver>()!;
    }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value is T data && IsCorrect(data))
        {
            return ValidationResult.ValidResult;
        }

        if (ResourceResolver.TryResolveString(InvalidResultString, out var @string))
        {
            return new ValidationResult(false, @string);
        }

        return new ValidationResult(false, null);
    }

    public abstract bool IsCorrect(T value);

}
