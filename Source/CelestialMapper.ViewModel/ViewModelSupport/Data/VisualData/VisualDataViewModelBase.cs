
using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

public abstract class VisualDataViewModelBase : ViewModelBase, IVisualData
{
    private ObservableCollection<IPropertyWrapper>? properties;

    public VisualDataViewModelBase(IViewModelSupport viewModelSupport) : base(viewModelSupport)
    {
        IsVisible.Value = true;
        InitializeProperties();
    }

    public ObservableCollection<IPropertyWrapper> Properties
    {
        get => this.properties ??= new();
        set => this.properties = value;
    }

    public PropertyWrapper<double> X { get; } = new(nameof(X));

    public PropertyWrapper<double> Y { get; } = new(nameof(Y));

    public PropertyWrapper<double> Width { get; } = new(nameof(Width));

    public PropertyWrapper<double> Height { get; } = new(nameof(Height));

    public PropertyWrapper<string> BackgroundColor { get; } = new(nameof(BackgroundColor));

    public PropertyWrapper<string> BorderColor { get; } = new(nameof(BorderColor));

    public PropertyWrapper<double> BorderThickness { get; } = new(nameof(BorderThickness));

    public PropertyWrapper<bool> IsVisible { get; } = new(nameof(IsVisible));

    public PropertyWrapper<int> ZIndex { get; } = new(nameof(ZIndex));

    public virtual void InitializeProperties()
    {
        this.Properties.Clear();
        this.Properties.AddRange(new IPropertyWrapper[]
        {
            X,
            Y,
            Width,
            Height,
            BackgroundColor,
            BorderColor,
            BorderThickness,
            IsVisible,
            ZIndex,
        });
    }
}
