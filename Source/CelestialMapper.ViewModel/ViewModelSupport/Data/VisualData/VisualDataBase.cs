using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

public abstract class VisualDataBase : NotifyPropertyChangedBase, IVisualData
{
    private ObservableCollection<IPropertyWrapper>? properties;

    public ObservableCollection<IPropertyWrapper> Properties
    {
        get => this.properties ??= new();
        set => this.properties = value;
    }

    public PropertyWrapper<double> X { get; private set; } = new(nameof(X));

    public PropertyWrapper<double> Y { get; private set; } = new(nameof(Y));

    public PropertyWrapper<double> Width { get; private set; } = new(nameof(Width));

    public PropertyWrapper<double> Height { get; private set; } = new(nameof(Height));

    public PropertyWrapper<string> BackgroundColor { get; private set; } = new(nameof(BackgroundColor));

    public PropertyWrapper<string> BorderColor { get; private set; } = new(nameof(BorderColor));

    public PropertyWrapper<double> BorderThickness { get; private set; } = new(nameof(BorderThickness));

    public PropertyWrapper<bool> IsVisible { get; private set; } = new(nameof(IsVisible));

    public PropertyWrapper<int> ZIndex { get; private set; } = new(nameof(ZIndex));

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
            ZIndex,
        });
    }

    protected void ReplaceProperty<T>(PropertyWrapper<T> oldProperty, PropertyWrapper<T> newProperty)
    {
        if (Properties.Contains(oldProperty))
        {
            int index = Properties.IndexOf(oldProperty);
            Properties.Remove(oldProperty);
            Properties.Insert(index, newProperty);
            return;
        }

        Properties.Add(newProperty);
    }
}
