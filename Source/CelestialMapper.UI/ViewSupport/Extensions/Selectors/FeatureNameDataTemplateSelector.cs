namespace CelestialMapper.UI;

public class FeatureNameDataTemplateSelector : PlatformDataTemplateSelector<FeatureNameDataTemplate>
{

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

        var result = Templates.SingleOrDefault(x => x.FeatureName == id);

        if (result is not null)
        {
            return result;
        }

        return GetDefault()!;
    }

}

public class FeatureNameDataTemplate : PlatformDataTemplate
{

    public string FeatureName { get; set; } = string.Empty;

}