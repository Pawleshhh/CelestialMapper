using System.Windows.Input;

namespace CelestialMapper.ViewModel;

public class OverlayToolData : NotifyPropertyChangedBase
{

    public OverlayToolData()
    {
        IsNotActive = true;
    }

    public FeatureName? OverlayTool
    {
        get => GetPropertyValue<FeatureName>();
        set => SetPropertyValue(value);
    }

    public object? Data
    {
        get => GetPropertyValue<object?>();
        set => SetPropertyValue<object?>(value);
    }

    public bool IsActive
    {
        get => GetPropertyValue<bool>();
        set
        {
            if (SetPropertyValue(value))
            {
                IsNotActive = !value;
            }
        }
    }

    public bool IsNotActive
    {
        get => GetPropertyValue<bool>();
        set
        {
            if (SetPropertyValue(value))
            {
                IsActive = !value;
            }
        }
    }

    private ICommand? exitOverlayCommand;
    public ICommand ExitOverlayCommand => this.exitOverlayCommand ??= new RelayCommand(o =>
    {
        IsActive = false;
    });

    public void SetTool(bool active, FeatureName? tool, object? data = null)
    {
        IsActive = active;
        OverlayTool = tool;
        Data = data;
    }

}
