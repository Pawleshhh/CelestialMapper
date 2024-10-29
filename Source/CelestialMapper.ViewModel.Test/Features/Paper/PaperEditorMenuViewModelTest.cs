using CelestialMapper.Common;
using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperEditorMenuViewModelTest : ViewModelTest<PaperEditorMenuViewModel>
{

    public Mock<IPaperEditor> PaperEditor { get; } = new();

    public override Func<PaperEditorMenuViewModel> CreateSUT => () => new(PaperEditor.Object, ViewModelSupport.Object);

    public override FeatureName DefaultFeatureName => FeatureNames.PaperEditorMenu;

    [Test]
    public void Initialize_SelectedPaperItem_IsNull()
    {
        // Arrange
        var sut = CreateSUT();

        // Act
        sut.Initialize(DefaultViewModelConfigurator);

        // Assert
        Assert.That(sut.SelectedPaperItem, Is.Null);
    }

    [Test]
    public void PaperItemSelected_WhenItemSelected_SetSelectedPaperItem()
    {
        // Arrange
        var sut = CreateSUTAndInitialize();
        var paperItem = new Mock<IPaperItem>();
        paperItem.SetupGet(x => x.IsSelected).Returns(true);

        // Act
        PaperEditor
            .Raise(x => x.PaperItemSelected += null, new PlatformEventArgs<IPaperItem>(paperItem.Object));

        // Assert
        Assert.That(sut.SelectedPaperItem, Is.SameAs(paperItem.Object));
    }

    [Test]
    public void PaperItemSelected_WhenItemNotSelected_SetSelectedPaperItemToNull()
    {
        // Arrange
        var sut = CreateSUTAndInitialize();
        var paperItem = new Mock<IPaperItem>();
        paperItem.SetupGet(x => x.IsSelected).Returns(false);

        // Act
        PaperEditor
            .Raise(x => x.PaperItemSelected += null, new PlatformEventArgs<IPaperItem>(paperItem.Object));

        // Assert
        Assert.That(sut.SelectedPaperItem, Is.Null);
    }

    [Test]
    public void PaperItemSelected_WhenItemIsNull_SetSelectedPaperItemToNull()
    {
        // Arrange
        var sut = CreateSUTAndInitialize();

        // Act
        PaperEditor
            .Raise(x => x.PaperItemSelected += null, new PlatformEventArgs<IPaperItem>());

        // Assert
        Assert.That(sut.SelectedPaperItem, Is.Null);
    }
}
