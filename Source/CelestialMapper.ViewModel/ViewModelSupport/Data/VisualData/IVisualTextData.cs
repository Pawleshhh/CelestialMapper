namespace CelestialMapper.ViewModel;

public interface IVisualTextData : IVisualData
{
    /// <summary>
    /// Gets or sets the text content.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the font size of the text.
    /// </summary>
    public double FontSize { get; set; }

    /// <summary>
    /// Gets or sets the font color of the text.
    /// </summary>
    public string FontColor { get; set; }

    /// <summary>
    /// Gets or sets the font family of the text.
    /// </summary>
    public string FontFamily { get; set; }

    /// <summary>
    /// Gets or sets the horizontal alignment of the text.
    /// </summary>
    public TextAlignment HorizontalAlignment { get; set; }

    /// <summary>
    /// Gets or sets the vertical alignment of the text.
    /// </summary>
    public TextAlignment VerticalAlignment { get; set; }
}

public enum TextAlignment
{
    Left,
    Center,
    Right,
    Justify
}