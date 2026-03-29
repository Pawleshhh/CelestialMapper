namespace CelestialMapper.ViewModel;

[Export(typeof(TextItem), IsSingleton = false, Key = nameof(TextItem))]
[PaperItemIdentifier(Category = PaperItemCategory.Text, ItemType = PaperItemType.Text, NameKey = nameof(TextItem))]
public class TextItem : PaperItemBase
{

    public override PaperItemType ItemType => PaperItemType.Text;

    public PropertyWrapper<string> Text { get; } = new(nameof(Text));

    // Font Characteristics
    public PropertyWrapper<double> FontSize { get; } = new(nameof(FontSize));

    public PropertyWrapper<string> FontFamily { get; } = new(nameof(FontFamily));

    public PropertyWrapper<bool> IsBold { get; } = new(nameof(IsBold));

    public PropertyWrapper<bool> IsItalic { get; } = new(nameof(IsItalic));

    public PropertyWrapper<bool> IsUnderline { get; } = new(nameof(IsUnderline));

    public PropertyWrapper<bool> IsStrikethrough { get; } = new(nameof(IsStrikethrough));

    public PropertyWrapper<int> FontWeight { get; } = new(nameof(FontWeight));

    // Text Alignment
    public PropertyWrapper<TextHorizontalAlignment> HorizontalAlignment { get; } = new(nameof(HorizontalAlignment));

    public PropertyWrapper<TextVerticalAlignment> VerticalAlignment { get; } = new(nameof(VerticalAlignment));

    // Text Layout
    public PropertyWrapper<bool> IsTextWrapped { get; } = new(nameof(IsTextWrapped));

    public PropertyWrapper<double> LineHeight { get; } = new(nameof(LineHeight));

    public PropertyWrapper<double> LetterSpacing { get; } = new(nameof(LetterSpacing));

    // Edit Mode
    public PropertyWrapper<bool> IsEditing { get; } = new(nameof(IsEditing));

    protected override void InitializeProperties()
    {
        base.InitializeProperties();
        this.Properties.AddRange(new IPropertyWrapper[]
        {
            Text,
            FontSize,
            FontFamily,
            IsBold,
            IsItalic,
            IsUnderline,
            IsStrikethrough,
            FontWeight,
            HorizontalAlignment,
            VerticalAlignment,
            IsTextWrapped,
            LineHeight,
            LetterSpacing,
            IsEditing
        });
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
