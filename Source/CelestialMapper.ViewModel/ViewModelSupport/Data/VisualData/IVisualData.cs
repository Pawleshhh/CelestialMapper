using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

public interface IVisualData
{
    /// <summary>
    /// Gets or sets the X coordinate of the visual data.
    /// </summary>
    public PropertyWrapper<double> X { get; }

    /// <summary>
    /// Gets or sets the Y coordinate of the visual data.
    /// </summary>
    public PropertyWrapper<double> Y { get; }

    /// <summary>
    /// Gets or sets the width of the visual data.
    /// </summary>
    public PropertyWrapper<double> Width { get; }

    /// <summary>
    /// Gets or sets the height of the visual data.
    /// </summary>
    public PropertyWrapper<double> Height { get; }

    /// <summary>
    /// Gets or sets the background color of the visual data.
    /// </summary>
    public PropertyWrapper<string> BackgroundColor { get; }

    /// <summary>
    /// Gets or sets the border color of the visual data.
    /// </summary>
    public PropertyWrapper<string> BorderColor { get; }

    /// <summary>
    /// Gets or sets the border thickness of the visual data.
    /// </summary>
    public PropertyWrapper<double> BorderThickness { get; }

    /// <summary>
    /// Gets or sets the visibility of the visual data.
    /// </summary>
    public PropertyWrapper<bool> IsVisible { get; }

    /// <summary>
    /// ZIndex of the visual data.
    /// </summary>
    public PropertyWrapper<int> ZIndex { get; }

    /// <summary>
    /// Properties listed.
    /// </summary>
    public ObservableCollection<IPropertyWrapper> Properties { get; }

    public void InitializeProperties();
}
