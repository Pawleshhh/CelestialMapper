using System.ComponentModel;

namespace CelestialMapper.ViewModel;

public interface IPropertyWrapper : INotifyPropertyChanged
{
    public string? Name { get; }
    public object? Value { get; set; }

    public bool IsReadOnly { get; set; }
}

public class PropertyWrapper<T> : NotifyPropertyChangedBase, IPropertyWrapper
{

    private Action<T?>? onBeforeSetValue;
    private Action<T?>? onAfterSetValue;

    public PropertyWrapper()
    {
        
    }

    public PropertyWrapper(string? name)
    {
        Name = name;
    }

    public PropertyWrapper(T value, string? name)
        : this(name)
    {
        Value = value;
    }

    public string? Name { get; }

    public bool IsReadOnly
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public T? Value
    {
        get
        {
            return GetPropertyValue<T?>();
        }
        set
        {
            this.onBeforeSetValue?.Invoke(value);
            SetPropertyValue(value);
            this.onAfterSetValue?.Invoke(value);
        }
    }

    object? IPropertyWrapper.Value
    {
        get => Value;
        set => Value = (T?)value;
    }

    public void SetupDelegates(Action<T?>? onBeforeSetValue, Action<T?>? onAfterSetValue)
    {
        this.onBeforeSetValue = onBeforeSetValue;
        this.onAfterSetValue = onAfterSetValue;
    }
}
