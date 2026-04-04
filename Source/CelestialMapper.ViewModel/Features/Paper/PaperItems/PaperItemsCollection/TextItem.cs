namespace CelestialMapper.ViewModel;

[Export(typeof(TextItem), IsSingleton = false, Key = nameof(TextItem))]
[PaperItemIdentifier(Category = PaperItemCategory.Text, ItemType = PaperItemType.Text, NameKey = nameof(TextItem))]
public class TextItem : PaperItemBase
{

    private readonly IFontService fontService;

    public TextItem(IFontService fontService)
    {
        this.fontService = fontService;
    }

    public override PaperItemType ItemType => PaperItemType.Text;

    public PropertyWrapper<string> Text { get; } = new("Text", nameof(Text));

    // Font Characteristics
    public PropertyWrapper<double> FontSize { get; } = new(16, nameof(FontSize));

    public ChoicePropertyWrapper<string> FontFamily { get; private set; } = default!;

    public PropertyWrapper<bool> IsBold { get; } = new(nameof(IsBold));

    public PropertyWrapper<bool> IsItalic { get; } = new(nameof(IsItalic));

    // Text Alignment
    public ChoicePropertyWrapper<TextHorizontalAlignment> HorizontalAlignment { get; private set; } = default!;

    public ChoicePropertyWrapper<TextVerticalAlignment> VerticalAlignment { get; private set; } = default!;

    // Text Layout
    public PropertyWrapper<bool> IsTextWrapped { get; } = new(nameof(IsTextWrapped));

    // Edit Mode
    public PropertyWrapper<bool> IsEditing { get; } = new(nameof(IsEditing));

    public override void InitializeProperties()
    {
        base.InitializeProperties();

        FontFamily = new(null!, nameof(FontFamily), this.fontService.GetFonts());
        HorizontalAlignment = new(default, nameof(HorizontalAlignment), Enum.GetValues<TextHorizontalAlignment>());
        VerticalAlignment = new(default, nameof(TextVerticalAlignment), Enum.GetValues<TextVerticalAlignment>());

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
