using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using CelestialMapper.ViewModel;

namespace CelestialMapper.UI;

/// <summary>
/// Selects the appropriate DataTemplate for editing a property value based on its type.
/// </summary>
public class PropertyValueEditorSelector : DataTemplateSelector
{
    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is not IPropertyWrapper wrapper || wrapper.Value == null)
        {
            // Return default read-only template for null values
            if (container is FrameworkElement element)
            {
                return element.FindResource("Template.PropertyEditor.ReadOnly") as DataTemplate ?? new DataTemplate();
            }
            return new DataTemplate();
        }

        var valueType = wrapper.Value.GetType();

        // Handle nullable types
        var baseType = Nullable.GetUnderlyingType(valueType) ?? valueType;

        string templateKey = baseType switch
        {
            _ when baseType == typeof(string) => "Template.PropertyEditor.String",
            _ when baseType == typeof(double) => "Template.PropertyEditor.Double",
            _ when baseType == typeof(float) => "Template.PropertyEditor.Double",
            _ when baseType == typeof(int) => "Template.PropertyEditor.Int",
            _ when baseType == typeof(long) => "Template.PropertyEditor.Int",
            _ when baseType == typeof(bool) => "Template.PropertyEditor.Bool",
            _ when baseType == typeof(DateTime) => "Template.PropertyEditor.DateTime",
            _ when baseType == typeof(TimeSpan) => "Template.PropertyEditor.TimeSpan",
            _ when baseType.IsEnum => "Template.PropertyEditor.Enum",
            _ => "Template.PropertyEditor.ReadOnly"
        };

        if (container is FrameworkElement frameworkElement)
        {
            return frameworkElement.FindResource(templateKey) as DataTemplate ?? new DataTemplate();
        }

        return new DataTemplate();
    }
}
