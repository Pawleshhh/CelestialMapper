namespace CelestialMapper.UI;

public class DataTypeTemplateSelector : PlatformDataTemplateSelector<PlatformDataTemplateSelectorItem>
{

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is null)
        {
            return base.SelectTemplate(item, container);
        }

        var itemType = item.GetType();
        var exactTemplate = Templates.SingleOrDefault(template => (Type)template.DataTemplate.DataType == itemType);
        
        if (exactTemplate is not null)
        {
            return exactTemplate.DataTemplate;
        }

        var relatedByInheritanceTemplate = Templates.SingleOrDefault(template => ((Type)template.DataTemplate.DataType).IsAssignableFrom(itemType));

        if (relatedByInheritanceTemplate is not null)
        {
            return relatedByInheritanceTemplate.DataTemplate;
        }

        return GetDefault();
    }

}