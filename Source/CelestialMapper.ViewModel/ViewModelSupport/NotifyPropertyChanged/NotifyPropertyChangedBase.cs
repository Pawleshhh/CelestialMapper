namespace CelestialMapper.ViewModel;

using FunctionalCSharp;
using System.ComponentModel;
using System.Runtime.CompilerServices;

public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged
{

    #region Fields

    private readonly Dictionary<string, object?> properties = new();

    #endregion

    #region Get/Set property

    public T? GetPropertyValue<T>([CallerMemberName] string propertyName = "")
    {
        if (this.properties.ContainsKey(propertyName))
        {
            return (T?)this.properties[propertyName];
        }
        
        return default;
    }

    public bool SetPropertyValue<T>(T? value, [CallerMemberName] string propertyName = "")
    {
        if (this.properties.ContainsKey(propertyName))
        {
            return 
                (this.properties[propertyName]?.Equals(value) ?? value is null)
                .Into(equal =>
                {
                    this.properties[propertyName] = value;
                    RisePropertyChanged(propertyName);
                    return !equal;
                });
        }
        
        if (Object.Equals(value, default(T?)))
        {
            return false;
        }

        this.properties.Add(propertyName, value);
        RisePropertyChanged(propertyName);
        return true;
    }

    #endregion


    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    public void RisePropertyChanged(params string[] properties)
        => properties.ForEach(OnPropertyChanged);

    protected virtual void OnPropertyChanged(string propertyName)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

}
