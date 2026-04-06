namespace CelestialMapper.UI;

public abstract class CelestialUIObject<T> : FrameworkElement
    where T : CelestialObjectVisualData
{

    public T VisualData { get; }

    protected CelestialUIObject(T visualData)
    {
        VisualData = visualData;
        Loaded += CelestialObjectUiObject_Loaded;
        Unloaded += CelestialObjectUiObject_Unloaded;
    }

    private void CelestialObjectUiObject_Unloaded(object sender, RoutedEventArgs e)
    {
        VisualData.PropertyChanged += VisualData_PropertyChanged;
    }

    private void CelestialObjectUiObject_Loaded(object sender, RoutedEventArgs e)
    {
        VisualData.PropertyChanged -= VisualData_PropertyChanged;
    }

    private void VisualData_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvalidateVisual();
    }
}
