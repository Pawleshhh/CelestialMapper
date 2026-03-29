namespace CelestialMapper.ViewModel;

[Export(typeof(TextItem), IsSingleton = false, Key = nameof(TextItem))]
[PaperItemIdentifier(Category = PaperItemCatergory.Text, ItemType = PaperItemType.Text, NameKey = nameof(TextItem))]
public class TextItem : PaperItemBase
{

    public override PaperItemType ItemType => PaperItemType.Text;

    public string Text
    {
        get => GetPropertyValue<string>() ?? string.Empty;
        set => SetPropertyValue(value);
    }

    // Font Characteristics
    public double FontSize
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public string FontFamily
    {
        get => GetPropertyValue<string>() ?? "Arial";
        set => SetPropertyValue(value);
    }

    public bool IsBold
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public bool IsItalic
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public bool IsUnderline
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public bool IsStrikethrough
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public int FontWeight
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }

    // Text Alignment
    public TextHorizontalAlignment HorizontalAlignment
    {
        get => GetPropertyValue<TextHorizontalAlignment>();
        set => SetPropertyValue(value);
    }

    public TextVerticalAlignment VerticalAlignment
    {
        get => GetPropertyValue<TextVerticalAlignment>();
        set => SetPropertyValue(value);
    }

    // Text Layout
    public bool IsTextWrapped
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public double LineHeight
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    public double LetterSpacing
    {
        get => GetPropertyValue<double>();
        set => SetPropertyValue(value);
    }

    // Edit Mode
    public bool IsEditing
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }
}

public enum TextHorizontalAlignment
{
    Left = 0,
    Center = 1,
    Right = 2,
    Justify = 3
}

public enum TextVerticalAlignment
{
    Top = 0,
    Center = 1,
    Bottom = 2
}
