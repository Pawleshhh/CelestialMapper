using System.Windows.Input;

namespace CelestialMapper.UI;

public interface ICelestialUIObject
{
    public CelestialObjectVisualData VisualData { get; }
}

public abstract class CelestialUIObject<T> : FrameworkElement, ICelestialUIObject
    where T : CelestialObjectVisualData
{

    public T VisualData { get; }

    CelestialObjectVisualData ICelestialUIObject.VisualData => VisualData;

    protected CelestialUIObject(T visualData)
    {
        VisualData = visualData;
        Loaded += CelestialObjectUiObject_Loaded;

    }

    private void CelestialObjectUiObject_Loaded(object sender, RoutedEventArgs e)
    {
        VisualData.PropertyChanged += VisualData_PropertyChanged;
        Unloaded += CelestialObjectUiObject_Unloaded;
    }

    private void CelestialObjectUiObject_Unloaded(object sender, RoutedEventArgs e)
    {
        VisualData.PropertyChanged -= VisualData_PropertyChanged;
        Loaded -= CelestialObjectUiObject_Loaded;
        Unloaded -= CelestialObjectUiObject_Unloaded;
    }

    private void VisualData_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        InvalidateVisual();
    }
}
