using System.ComponentModel;

namespace CelestialMapper.ViewModel;

public interface IPropertyWrapper : INotifyPropertyChanged
{
    public string? Name { get; }
    public object? Value { get; set; }
}

public class PropertyWrapper<T> : NotifyPropertyChangedBase, IPropertyWrapper
{
    public PropertyWrapper()
    {
        
    }

    public PropertyWrapper(string? name)
    {
        Name = name;
    }

    public string? Name { get; }

    public T? Value
    {
        get => GetPropertyValue<T?>();
        set => SetPropertyValue(value);
    }

    object? IPropertyWrapper.Value
    {
        get => Value;
        set => Value = (T?)value;
    }
}
