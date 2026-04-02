namespace CelestialMapper.UI;

using System.Reflection;

public class DataTypeTemplateSelector : PlatformDataTemplateSelector<PlatformDataTemplateSelectorItem>
{

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is null)
        {
            return base.SelectTemplate(item, container);
        }

        var itemType = item.GetType();
        var exactTemplate = Templates.FirstOrDefault(template => (Type)template.DataTemplate.DataType == itemType);

        if (exactTemplate is not null)
        {
            return exactTemplate.DataTemplate;
        }

        var genericTemplate = Templates.FirstOrDefault(template => MatchesGenericType(itemType, (Type)template.DataTemplate.DataType));

        if (genericTemplate is not null)
        {
            return genericTemplate.DataTemplate;
        }

        var relatedByInheritanceTemplate = Templates.SingleOrDefault(template => ((Type)template.DataTemplate.DataType).IsAssignableFrom(itemType));

        if (relatedByInheritanceTemplate is not null)
        {
            return relatedByInheritanceTemplate.DataTemplate;
        }

        return GetDefault()!;
    }

    private static bool MatchesGenericType(Type itemType, Type templateType)
    {
        if (itemType.IsGenericType && templateType.IsGenericTypeDefinition)
        {
            return itemType.GetGenericTypeDefinition() == templateType;
        }

        if (itemType.IsGenericType && templateType.IsGenericType)
        {
            var itemGenericDef = itemType.GetGenericTypeDefinition();
            var templateGenericDef = templateType.GetGenericTypeDefinition();
            if (itemGenericDef == templateGenericDef)
            {
                return true;
            }

            foreach (var baseType in GetBaseTypes(itemType))
            {
                if (baseType.IsGenericType && baseType.GetGenericTypeDefinition() == templateGenericDef)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static IEnumerable<Type> GetBaseTypes(Type type)
    {
        var current = type.BaseType;
        while (current is not null)
        {
            yield return current;
            current = current.BaseType;
        }

        foreach (var interfaceType in type.GetInterfaces())
        {
            yield return interfaceType;
        }
    }

}