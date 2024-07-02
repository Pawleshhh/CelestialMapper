using CelestialMapper.ViewModel;
using System.Windows;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class FeatureNameDataTemplateSelectorTest
{
    [Test]
    public void SelectTemplate_NullItem_ReturnsBaseTemplate()
    {
        var selector = new FeatureNameDataTemplateSelector();
        var container = new DependencyObject();

        var result = selector.SelectTemplate(null!, container);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void SelectTemplate_NonStringItem_ThrowsArgumentException()
    {
        var selector = new FeatureNameDataTemplateSelector();
        var container = new DependencyObject();
        var item = new object(); // Non-string item

        Assert.Throws<ArgumentException>(() => selector.SelectTemplate(item, container));
    }

    [Test]
    public void SelectTemplate_ValidItem_ReturnsMatchingTemplate()
    {
        var selector = new FeatureNameDataTemplateSelector();
        var container = new DependencyObject();

        var template1 = new FeatureNameDataTemplate { FeatureName = new("Feature1") };
        var template2 = new FeatureNameDataTemplate { FeatureName = new("Feature2") };

        selector.Templates.Add(template1);
        selector.Templates.Add(template2);

        var result = selector.SelectTemplate(new FeatureName("Feature2"), container);

        Assert.That(result, Is.SameAs(template2.DataTemplate));
    }

    [Test]
    public void SelectTemplate_ItemNotFound_ThrowsInvalidOperationException()
    {
        var selector = new FeatureNameDataTemplateSelector();
        var container = new DependencyObject();

        var template1 = new FeatureNameDataTemplate { FeatureName = new("Feature1") };

        selector.Templates.Add(template1);

        var result = selector.SelectTemplate(new FeatureName("Feature2"), container);

        Assert.That(result, Is.Null);
    }
}
