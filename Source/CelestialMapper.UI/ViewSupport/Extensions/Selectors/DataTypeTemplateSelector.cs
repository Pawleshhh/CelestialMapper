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

        // Try exact type match
        var template = Templates.FirstOrDefault(t => TemplateTypeMatches(itemType, t, exact: true));
        if (template is not null)
        {
            return template.DataTemplate;
        }

        // Try generic type match
        template = Templates.FirstOrDefault(t => TemplateTypeMatches(itemType, t, exact: false));
        if (template is not null)
        {
            return template.DataTemplate;
        }

        // Try inheritance-based match (skip for generic types to avoid issues)
        if (!itemType.IsGenericType)
        {
            template = Templates.SingleOrDefault(t => IsAssignableFromTemplate(itemType, t));
            if (template is not null)
            {
                return template.DataTemplate;
            }
        }

        return GetDefault()!;
    }

    private static Type? GetTemplateType(PlatformDataTemplateSelectorItem template)
    {
        return template.DataTemplate?.DataType as Type;
    }

    private static bool TemplateTypeMatches(Type itemType, PlatformDataTemplateSelectorItem template, bool exact)
    {
        var templateType = GetTemplateType(template);
        if (templateType is null)
        {
            return false;
        }

        return exact ? templateType == itemType : MatchesGenericType(itemType, templateType);
    }

    private static bool IsAssignableFromTemplate(Type itemType, PlatformDataTemplateSelectorItem template)
    {
        var templateType = GetTemplateType(template);
        return templateType is not null && templateType.IsAssignableFrom(itemType);
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