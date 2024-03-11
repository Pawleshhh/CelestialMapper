using System.Collections.ObjectModel;
using System.Windows.Markup;

namespace CelestialMapper.UI;

[ContentProperty(nameof(Templates))]
public class FeatureNameDataTemplateSelector : DataTemplateSelector
{

    public ObservableCollection<FeatureNameDataTemplate> Templates { get; } = new();

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is null)
        {
            return base.SelectTemplate(item, container);
        }

        if (item is not string id)
        {
            throw new ArgumentException($"Item is expected to be string with feature name but received {item.GetType()}", nameof(item));
        }

        return Templates.Single(x => x.FeatureName == id);
    }

}

public class FeatureNameDataTemplate : DataTemplate
{

    public string FeatureName { get; set; } = string.Empty;

}