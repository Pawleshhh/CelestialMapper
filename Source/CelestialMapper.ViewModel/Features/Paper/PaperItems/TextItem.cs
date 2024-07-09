
namespace CelestialMapper.ViewModel;

[Export(typeof(TextItem), IsSingleton = false, Key = nameof(TextItem))]
public class TextItem : PaperItemBase
{
    public string Text
    {
        get => GetPropertyValue<string>() ?? string.Empty;
        set => SetPropertyValue(value);
    }
}
