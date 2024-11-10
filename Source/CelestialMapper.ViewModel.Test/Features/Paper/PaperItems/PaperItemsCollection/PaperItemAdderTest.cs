using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperItemAdderTest
{
    private Mock<IPaperEditor> paperEditorMock;

    [SetUp]
    public void SetUp()
    {
        this.paperEditorMock = new Mock<IPaperEditor>(MockBehavior.Strict);
    }

    [Test]
    public void Constructor_ShouldInitializeWithExpectedProperties()
    {
        // Arrange
        var paperItemAdder = new PaperItemAdder(this.paperEditorMock.Object)
        {
            PaperItemType = PaperItemType.Text
        };

        // Act & Assert
        Assert.That(paperItemAdder.PaperItemType, Is.EqualTo(PaperItemType.Text), "PaperItemType should be set correctly.");
        Assert.That(paperItemAdder.CreateCommand, Is.Not.Null, "CreateCommand should be initialized.");
    }

    [Test]
    public void Create_ShouldCallAddPaperItemWithCorrectType_WhenExecuted()
    {
        // Arrange
        var paperItemAdder = new PaperItemAdder(this.paperEditorMock.Object)
        {
            PaperItemType = PaperItemType.Map
        };

        this.paperEditorMock.Setup(editor => editor.AddPaperItem(PaperItemType.Map)).Verifiable();

        // Act
        paperItemAdder.CreateCommand.Execute(null);

        // Assert
        this.paperEditorMock.Verify(editor => editor.AddPaperItem(PaperItemType.Map), Times.Once, "AddPaperItem should be called once with correct PaperItemType.");
    }
}
