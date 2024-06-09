namespace CelestialMapper.UI;

public class DataTypeTemplateSelector : PlatformDataTemplateSelector<PlatformDataTemplate>
{

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is null)
        {
            return base.SelectTemplate(item, container);
        }

        var itemType = item.GetType();
        var exactTemplate = Templates.SingleOrDefault(template => (Type)template.DataType == itemType);
        
        if (exactTemplate is not null)
        {
            return exactTemplate;
        }

        var relatedByInheritanceTemplate = Templates.SingleOrDefault(template => ((Type)template.DataType).IsAssignableFrom(itemType));

        if (relatedByInheritanceTemplate is not null)
        {
            return relatedByInheritanceTemplate;
        }

        return GetDefault()!;
    }

}