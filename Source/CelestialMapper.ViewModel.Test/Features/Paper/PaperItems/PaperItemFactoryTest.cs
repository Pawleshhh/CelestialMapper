using CelestialMapper.Common;
using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperItemFactoryTest
{
    private PaperItemFactory paperItemFactory;
    private Mock<IPaperItemContextMenuFactory> mockContextMenuFactory;
    private Mock<IResourceResolver> mockResourceResolver;
    private Mock<IIoCManager> mockIoCManager;

    [SetUp]
    public void SetUp()
    {
        this.mockContextMenuFactory = new Mock<IPaperItemContextMenuFactory>();
        this.mockResourceResolver = new Mock<IResourceResolver>();
        this.mockIoCManager = new Mock<IIoCManager>();
        this.paperItemFactory = new PaperItemFactory(this.mockContextMenuFactory.Object, this.mockResourceResolver.Object, this.mockIoCManager.Object);
    }

    //[Test]
    //public void Create_WithTypeMap_ReturnsMapItemWithCommands()
    //{
    //    var mapItem = new Mock<MapViewModel>();
    //    this.mockIoCManager.Setup(ioc => ioc.ServiceProvider.GetService(typeof(MapViewModel))).Returns(mapItem.Object);

    //    var result = this.paperItemFactory.Create(PaperItemType.Map);

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(result, Is.EqualTo(mapItem.Object));
    //        this.mockContextMenuFactory.Verify(cm => cm.CreateCommands(mapItem.Object), Times.Once);
    //    });
    //}

    [Test]
    public void Create_WithTypeText_ReturnsTextItemWithCommands()
    {
        string expectedText = "Sample text";
        this.mockResourceResolver.Setup(rr => rr.TryResolveString("String.PaperItem.DefaultValue.Text", out expectedText)).Returns(true);

        var result = this.paperItemFactory.Create(PaperItemType.Text);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<TextItem>());
            Assert.That(((TextItem)result).Text, Is.EqualTo(expectedText));
            this.mockContextMenuFactory.Verify(cm => cm.CreateCommands(result), Times.Once);
        });
    }

    [Test]
    public void Create_WithUnhandledType_ThrowsInvalidOperationException()
    {
        Assert.That(() => this.paperItemFactory.Create((PaperItemType)99),
            Throws.InvalidOperationException.With.Message.EqualTo("Not expected type of paper item - 99"));
    }

    [Test]
    public void Create_WithTypeAndValue_ReturnsTextItemWithSpecifiedText()
    {
        const string customText = "Custom text";

        var result = this.paperItemFactory.Create(PaperItemType.Text, customText);

        Assert.Multiple(() =>
        {
            Assert.That(result, Is.TypeOf<TextItem>());
            Assert.That(((TextItem)result).Text, Is.EqualTo(customText));
            this.mockContextMenuFactory.Verify(cm => cm.CreateCommands(result), Times.Once);
        });
    }

    //[Test]
    //public void Create_WithTypeMapAndNullValue_ReturnsMapItemWithCommands()
    //{
    //    var mockMapItem = new Mock<IPaperItem>();
    //    this.mockIoCManager.Setup(ioc => ioc.ServiceProvider.GetService(typeof(MapViewModel))).Returns(mockMapItem.Object);

    //    var result = this.paperItemFactory.Create(PaperItemType.Map, null!);

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(result, Is.EqualTo(mockMapItem.Object));
    //        this.mockContextMenuFactory.Verify(cm => cm.CreateCommands(mockMapItem.Object), Times.Once);
    //    });
    //}

    [Test]
    public void GetResourceString_WhenResourceKeyExists_ReturnsResourceValue()
    {
        string key = "String.PaperItem.DefaultValue.Text";
        string expectedValue = "Sample text";
        this.mockResourceResolver.Setup(rr => rr.TryResolveString(key, out expectedValue)).Returns(true);

        var result = this.paperItemFactory.Create(PaperItemType.Text);

        Assert.That(((TextItem)result).Text, Is.EqualTo(expectedValue));
    }

    [Test]
    public void GetResourceString_WhenResourceKeyDoesNotExist_ReturnsEmptyString()
    {
        const string key = "String.PaperItem.DefaultValue.NonExistent";
        this.mockResourceResolver.Setup(rr => rr.TryResolveString(key, out It.Ref<string>.IsAny)).Returns(false);

        var result = this.paperItemFactory.Create(PaperItemType.Text);

        Assert.That(((TextItem)result).Text, Is.Empty);
    }
}
