namespace CelestialMapper.ViewModel;

public interface IVisualData
{
    /// <summary>
    /// Gets or sets the X coordinate of the visual data.
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate of the visual data.
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Gets or sets the width of the visual data.
    /// </summary>
    public double Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the visual data.
    /// </summary>
    public double Height { get; set; }

    /// <summary>
    /// Gets or sets the background color of the visual data.
    /// </summary>
    public string BackgroundColor { get; set; }

    /// <summary>
    /// Gets or sets the border color of the visual data.
    /// </summary>
    public string BorderColor { get; set; }

    /// <summary>
    /// Gets or sets the border thickness of the visual data.
    /// </summary>
    public double BorderThickness { get; set; }

    /// <summary>
    /// Gets or sets the visibility of the visual data.
    /// </summary>
    public bool IsVisible { get; set; }
}
