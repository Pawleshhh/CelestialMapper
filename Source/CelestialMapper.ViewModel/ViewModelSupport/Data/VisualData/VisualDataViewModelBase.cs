
namespace CelestialMapper.ViewModel;

public abstract class VisualDataViewModelBase : ViewModelBase, IVisualData
{

    public VisualDataViewModelBase(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        IsVisible = true;
    }

    public double X
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public double Y
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public double Width
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public double Height
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public string BackgroundColor
    {
        get => GetPropertyValue<string>() ?? string.Empty;
        set => SetPropertyValue(value);
    }

    public string BorderColor
    {
        get => GetPropertyValue<string>() ?? string.Empty;
        set => SetPropertyValue(value);
    }

    public double BorderThickness
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public bool IsVisible
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public int ZIndex
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }
}
