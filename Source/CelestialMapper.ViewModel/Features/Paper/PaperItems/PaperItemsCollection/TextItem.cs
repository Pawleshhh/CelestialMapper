namespace CelestialMapper.ViewModel;

[Export(typeof(TextItem), IsSingleton = false, Key = nameof(TextItem))]
[PaperItemIdentifier(Category = PaperItemCategory.Text, ItemType = PaperItemType.Text, NameKey = nameof(TextItem))]
public class TextItem : PaperItemBase
{

    public override PaperItemType ItemType => PaperItemType.Text;

    public PropertyWrapper<string> Text { get; } = new("Text", nameof(Text));

    // Font Characteristics
    public PropertyWrapper<double> FontSize { get; } = new(16, nameof(FontSize));

    public PropertyWrapper<string> FontFamily { get; } = new("Arial", nameof(FontFamily));

    public PropertyWrapper<bool> IsBold { get; } = new(nameof(IsBold));

    public PropertyWrapper<bool> IsItalic { get; } = new(nameof(IsItalic));

    // Text Alignment
    public PropertyWrapper<TextHorizontalAlignment> HorizontalAlignment { get; } = new(nameof(HorizontalAlignment));

    public PropertyWrapper<TextVerticalAlignment> VerticalAlignment { get; } = new(nameof(VerticalAlignment));

    // Text Layout
    public PropertyWrapper<bool> IsTextWrapped { get; } = new(nameof(IsTextWrapped));

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
            HorizontalAlignment,
            VerticalAlignment,
            IsTextWrapped
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
