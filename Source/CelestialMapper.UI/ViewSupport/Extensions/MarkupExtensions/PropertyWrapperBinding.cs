using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace CelestialMapper.UI;

/// <summary>
/// A custom binding markup extension that automatically binds to the .Value property of PropertyWrapper<T>.
/// Usage: {local:PropertyWrapperBinding Path=PropertyName}
/// </summary>
public class PropertyWrapperBinding : MarkupExtension
{
    private readonly Binding _binding;

    public PropertyWrapperBinding()
    {
        this._binding = new Binding();
    }

    public PropertyWrapperBinding(string path)
    {
        this._binding = new Binding($"{path}.Value");
    }

    #region Binding Properties

    [ConstructorArgument("path")]
    public string Path
    {
        get => this._binding.Path.Path;
        set => this._binding.Path = new System.Windows.PropertyPath($"{value}.Value");
    }

    public BindingMode Mode
    {
        get => this._binding.Mode;
        set => this._binding.Mode = value;
    }

    public UpdateSourceTrigger UpdateSourceTrigger
    {
        get => this._binding.UpdateSourceTrigger;
        set => this._binding.UpdateSourceTrigger = value;
    }

    public IValueConverter Converter
    {
        get => this._binding.Converter;
        set => this._binding.Converter = value;
    }

    public object ConverterParameter
    {
        get => this._binding.ConverterParameter;
        set => this._binding.ConverterParameter = value;
    }

    public string StringFormat
    {
        get => this._binding.StringFormat;
        set => this._binding.StringFormat = value;
    }

    public object FallbackValue
    {
        get => this._binding.FallbackValue;
        set => this._binding.FallbackValue = value;
    }

    public RelativeSource RelativeSource
    {
        get => this._binding.RelativeSource;
        set => this._binding.RelativeSource = value;
    }

    public object Source
    {
        get => this._binding.Source;
        set => this._binding.Source = value;
    }

    public Collection<ValidationRule> ValidationRules => this._binding.ValidationRules;

    #endregion

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        return this._binding.ProvideValue(serviceProvider);
    }
}
