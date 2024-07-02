namespace CelestialMapper.UI;

public class FeatureNameDataTemplateSelector : PlatformDataTemplateSelector<FeatureNameDataTemplate>
{

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is null)
        {
            return base.SelectTemplate(item, container);
        }

        if (item is not FeatureName id)
        {
            throw new ArgumentException($"Item is expected to be {typeof(FeatureName).Name} but received {item.GetType()}", nameof(item));
        }

        var result = Templates.SingleOrDefault(x => x.FeatureName == id);

        if (result is not null)
        {
            return result.DataTemplate;
        }

        return GetDefault()!;
    }

}

public class FeatureNameDataTemplate : PlatformDataTemplateSelectorItem
{

    public FeatureName FeatureName { get; set; } = FeatureName.Unknown;

}