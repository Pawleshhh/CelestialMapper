using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace CelestialMapper.UI;

[ContentProperty(nameof(Templates))]
public abstract class PlatformDataTemplateSelector<TemplateType> : DataTemplateSelector
    where TemplateType : PlatformDataTemplateSelectorItem
{

    public ObservableCollection<TemplateType> Templates { get; set; } = new();

    protected DataTemplate? GetDefault()
    {
        return Templates.FirstOrDefault(template => template.IsDefault)?.DataTemplate;
    }

}

public class PlatformDataTemplateSelectorItem
{

    public bool IsDefault { get; set; }

    public DataTemplate DataTemplate { get; set; } = default!;

}