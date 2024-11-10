using CelestialMapper.Common;
using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperItemsCollectionViewModelTest : ViewModelTest<PaperItemsCollectionViewModel>
{
    public override Func<PaperItemsCollectionViewModel> CreateSUT
        => () => new(ViewModelSupport.Object, PaperEditor.Object, AttributeRetrievalService.Object);

    public override FeatureName DefaultFeatureName => new("ToolboxMenu");

    public Mock<IPaperEditor> PaperEditor { get; } = new(MockBehavior.Strict);
    public Mock<IAttributeRetrievalService> AttributeRetrievalService { get; } = new(MockBehavior.Strict);

    [OneTimeSetUp]
    public override void OneTimeSetUp()
    {
        base.OneTimeSetUp(); 
        AttributeRetrievalService
            .Setup(service => service.GetAttributes<PaperItemIdentifierAttribute>(It.IsAny<System.Reflection.Assembly>(), false))
            .Returns(Enumerable.Empty<PaperItemIdentifierAttribute>());
    }

    [Test]
    public void Initialize_ShouldPopulateItemAdders_WhenPaperItemIdentifiersExist()
    {
        // Arrange: Mock the attribute retrieval to return dummy classes' attributes
        var attributes = new List<PaperItemIdentifierAttribute>
        {
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Map, ItemType = PaperItemType.Map, NameKey = "MapItem1" },
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Map, ItemType = PaperItemType.Map, NameKey = "MapItem2" },
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Text, ItemType = PaperItemType.Text, NameKey = "TextItem1" }
        };

        AttributeRetrievalService
            .Setup(service => service.GetAttributes<PaperItemIdentifierAttribute>(It.IsAny<System.Reflection.Assembly>(), false))
            .Returns(attributes);

        var sut = CreateSUT();

        // Act
        sut.Initialize(CreateMockConfigurator());

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(sut.ItemAdders, Has.Count.EqualTo(2), "There should be 2 unique categories in ItemAdders.");

            var mapCategoryAdder = sut.ItemAdders.FirstOrDefault(adder => adder.Category == PaperItemCatergory.Map);
            var textCategoryAdder = sut.ItemAdders.FirstOrDefault(adder => adder.Category == PaperItemCatergory.Text);

            Assert.That(mapCategoryAdder, Is.Not.Null, "ItemAdders should contain a 'Map' category.");
            Assert.That(mapCategoryAdder!.PaperItemAdders, Has.Count.EqualTo(2), "'Map' category should contain 2 PaperItemAdders.");

            Assert.That(textCategoryAdder, Is.Not.Null, "ItemAdders should contain a 'Text' category.");
            Assert.That(textCategoryAdder!.PaperItemAdders, Has.Count.EqualTo(1), "'Text' category should contain 1 PaperItemAdder.");
        });
    }

    [Test]
    public void Initialize_ShouldNotDuplicateCategories_WhenMultipleItemsWithSameCategoryExist()
    {
        // Arrange: Two items with the same category 'Map'
        var attributes = new List<PaperItemIdentifierAttribute>
        {
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Map, ItemType = PaperItemType.Map, NameKey = "MapItem1" },
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Map, ItemType = PaperItemType.Map, NameKey = "MapItem2" }
        };

        AttributeRetrievalService
            .Setup(service => service.GetAttributes<PaperItemIdentifierAttribute>(It.IsAny<System.Reflection.Assembly>(), false))
            .Returns(attributes);

        var sut = CreateSUT();

        // Act
        sut.Initialize(CreateMockConfigurator());

        // Assert
        Assert.That(sut.ItemAdders.Count(adder => adder.Category == PaperItemCatergory.Map), Is.EqualTo(1), "There should only be one adder for 'Map' category.");
    }

    [Test]
    public void Initialize_ShouldCreateItemAddersWithExpectedProperties()
    {
        // Arrange: Mock attributes for various categories and items
        var attributes = new List<PaperItemIdentifierAttribute>
        {
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Map, ItemType = PaperItemType.Map, NameKey = "MapItem1" },
            new PaperItemIdentifierAttribute { Category = PaperItemCatergory.Text, ItemType = PaperItemType.Text, NameKey = "TextItem1" }
        };

        AttributeRetrievalService
            .Setup(service => service.GetAttributes<PaperItemIdentifierAttribute>(It.IsAny<System.Reflection.Assembly>(), false))
            .Returns(attributes);

        var sut = CreateSUT();

        // Act
        sut.Initialize(CreateMockConfigurator());

        // Assert
        Assert.Multiple(() =>
        {
            foreach (var categoryAdder in sut.ItemAdders)
            {
                Assert.That(categoryAdder.PaperItemAdders, Is.Not.Empty, $"Category '{categoryAdder.Category}' should contain at least one PaperItemAdder.");
                Assert.That(categoryAdder.PaperItemAdders.All(adder => adder.PaperItemType != PaperItemType.Unknown), Is.True, "All PaperItemAdders should have a non-Unknown PaperItemType.");
            }
        });
    }

    private IViewModelConfigurator CreateMockConfigurator()
    {
        var configurator = new Mock<IViewModelConfigurator>();
        configurator.Setup(c => c.GetFeatureName()).Returns(DefaultFeatureName);
        return configurator.Object;
    }
}
