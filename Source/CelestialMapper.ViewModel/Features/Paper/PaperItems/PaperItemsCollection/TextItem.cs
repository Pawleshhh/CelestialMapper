
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
}
