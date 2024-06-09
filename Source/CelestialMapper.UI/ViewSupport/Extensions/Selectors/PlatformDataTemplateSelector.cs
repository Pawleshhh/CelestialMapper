using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace CelestialMapper.UI;

[ContentProperty(nameof(Templates))]
public abstract class PlatformDataTemplateSelector<TemplateType> : DataTemplateSelector
    where TemplateType : PlatformDataTemplate
{

    public ObservableCollection<TemplateType> Templates { get; } = new();

    protected TemplateType? GetDefault()
    {
        return Templates.FirstOrDefault(template => template.IsDefault);
    }

}

public class PlatformDataTemplate : DataTemplate
{

    public bool IsDefault { get; set; }

}