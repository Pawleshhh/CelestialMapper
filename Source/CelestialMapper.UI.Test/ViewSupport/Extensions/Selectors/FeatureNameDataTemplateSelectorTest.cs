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

        var template1 = new FeatureNameDataTemplate { FeatureName = "Feature1" };
        var template2 = new FeatureNameDataTemplate { FeatureName = "Feature2" };

        selector.Templates.Add(template1);
        selector.Templates.Add(template2);

        var result = selector.SelectTemplate("Feature2", container);

        Assert.That(result, Is.EqualTo(template2));
    }

    [Test]
    public void SelectTemplate_ItemNotFound_ThrowsInvalidOperationException()
    {
        var selector = new FeatureNameDataTemplateSelector();
        var container = new DependencyObject();

        var template1 = new FeatureNameDataTemplate { FeatureName = "Feature1" };

        selector.Templates.Add(template1);

        Assert.Throws<InvalidOperationException>(() => selector.SelectTemplate("Feature2", container));
    }
}
