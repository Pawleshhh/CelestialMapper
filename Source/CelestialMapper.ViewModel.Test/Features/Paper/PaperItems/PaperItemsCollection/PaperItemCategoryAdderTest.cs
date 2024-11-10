using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperItemCategoryAdderTest
{
    private Mock<IPaperEditor> paperEditorMock;

    [SetUp]
    public void SetUp()
    {
        this.paperEditorMock = new Mock<IPaperEditor>(MockBehavior.Strict);
    }

    [Test]
    public void Constructor_ShouldInitializeWithEmptyPaperItemAddersCollection()
    {
        // Arrange
        var categoryAdder = new PaperItemCategoryAdder(this.paperEditorMock.Object);

        // Act & Assert
        Assert.That(categoryAdder.PaperItemAdders, Is.Empty, "PaperItemAdders collection should be initialized as empty.");
    }

    [Test]
    public void CategoryProperty_ShouldSetAndReturnCorrectCategory()
    {
        // Arrange
        var categoryAdder = new PaperItemCategoryAdder(this.paperEditorMock.Object)
        {
            Category = PaperItemCatergory.Map
        };

        // Act & Assert
        Assert.That(categoryAdder.Category, Is.EqualTo(PaperItemCatergory.Map), "Category should be set correctly.");
    }

    [Test]
    public void PaperItemAddersCollection_ShouldAllowAddingPaperItemAdders()
    {
        // Arrange
        var categoryAdder = new PaperItemCategoryAdder(this.paperEditorMock.Object);
        var itemAdder = new PaperItemAdder(this.paperEditorMock.Object) { PaperItemType = PaperItemType.Map };

        // Act
        categoryAdder.PaperItemAdders.Add(itemAdder);

        // Assert
        Assert.That(categoryAdder.PaperItemAdders, Has.Count.EqualTo(1), "PaperItemAdders should contain one item after adding.");
        Assert.That(categoryAdder.PaperItemAdders[0], Is.EqualTo(itemAdder), "PaperItemAdders should contain the added item.");
    }
}