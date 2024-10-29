using Moq;
using System.ComponentModel;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperEditorTest
{
    private Mock<IPaperStorage> paperStorageMock;
    private Mock<IPaperItemFactory> paperItemFactoryMock;
    private Mock<IZIndexProcessor> zIndexProcessorMock;
    private PaperEditor paperEditor;
    private Mock<IPaperItem> paperItemMock;

    [SetUp]
    public void SetUp()
    {
        this.paperStorageMock = new Mock<IPaperStorage>();
        this.paperItemFactoryMock = new Mock<IPaperItemFactory>();
        this.zIndexProcessorMock = new Mock<IZIndexProcessor>();

        this.paperStorageMock.SetupGet(ps => ps.PaperItems).Returns(new Dictionary<Guid, IPaperItem>());

        this.paperEditor = new PaperEditor(
            this.paperStorageMock.Object,
            this.paperItemFactoryMock.Object,
            this.zIndexProcessorMock.Object
        );

        this.paperItemMock = new Mock<IPaperItem>();
    }

    [Test]
    public void AddPaperItem_WhenCalledWithItemType_ShouldAddItemToStorageAndTriggerEvent()
    {
        // Arrange
        var itemType = PaperItemType.Text;
        var item = this.paperItemMock.Object;
        var itemId = Guid.NewGuid();

        this.paperItemFactoryMock.Setup(f => f.Create(itemType)).Returns(item);
        this.paperItemMock.SetupGet(i => i.Id).Returns(itemId);

        bool eventTriggered = false;
        this.paperEditor.PaperItemAdded += (s, e) => eventTriggered = true;

        // Act
        this.paperEditor.AddPaperItem(itemType);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventTriggered, Is.True, "PaperItemAdded event was not triggered");
            Assert.That(this.paperStorageMock.Object.PaperItems.ContainsKey(itemId), Is.True, "Item was not added to storage");
            this.zIndexProcessorMock.Verify(z => z.ProcessNewItem(It.IsAny<IEnumerable<IPaperItem>>(), item), Times.Once);
        });
    }

    [Test]
    public void AddPaperItem_WhenCalledWithItemTypeAndValue_ShouldAddItemToStorageAndTriggerEvent()
    {
        // Arrange
        var itemType = PaperItemType.Text;
        var itemValue = new object();
        var item = this.paperItemMock.Object;
        var itemId = Guid.NewGuid();

        this.paperItemFactoryMock.Setup(f => f.Create(itemType, itemValue)).Returns(item);
        this.paperItemMock.SetupGet(i => i.Id).Returns(itemId);

        bool eventTriggered = false;
        this.paperEditor.PaperItemAdded += (s, e) => eventTriggered = true;

        // Act
        this.paperEditor.AddPaperItem(itemType, itemValue);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventTriggered, Is.True, "PaperItemAdded event was not triggered");
            Assert.That(this.paperStorageMock.Object.PaperItems.ContainsKey(itemId), Is.True, "Item was not added to storage");
            this.zIndexProcessorMock.Verify(z => z.ProcessNewItem(It.IsAny<IEnumerable<IPaperItem>>(), item), Times.Once);
        });
    }

    [Test]
    public void RemovePaperItem_WhenCalledWithExistingItemId_ShouldRemoveItemAndTriggerEvent()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        this.paperStorageMock.Object.PaperItems.Add(itemId, this.paperItemMock.Object);

        bool eventTriggered = false;
        this.paperEditor.PaperItemRemoved += (s, e) => eventTriggered = true;

        // Act
        this.paperEditor.RemovePaperItem(itemId);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(eventTriggered, Is.True, "PaperItemRemoved event was not triggered");
            Assert.That(this.paperStorageMock.Object.PaperItems.ContainsKey(itemId), Is.False, "Item was not removed from storage");
        });
    }

    [Test]
    public void RemovePaperItem_WhenCalledWithNonExistingItemId_ShouldNotTriggerEvent()
    {
        // Arrange
        bool eventTriggered = false;
        this.paperEditor.PaperItemRemoved += (s, e) => eventTriggered = true;

        // Act
        this.paperEditor.RemovePaperItem(Guid.NewGuid());

        // Assert
        Assert.That(eventTriggered, Is.False, "PaperItemRemoved event was triggered for a non-existing item");
    }

    [Test]
    public void OnPaperItemSelected_WhenItemIsSelected_ShouldTriggerPaperItemSelectedEvent()
    {
        // Arrange
        bool eventTriggered = false;
        this.paperEditor.PaperItemSelected += (s, e) => eventTriggered = true;
        this.paperItemMock.SetupGet(i => i.IsSelected).Returns(true);
        this.paperItemMock.SetupGet(i => i.Id).Returns(Guid.NewGuid());

        this.paperStorageMock.Object.PaperItems.Add(Guid.NewGuid(), this.paperItemMock.Object);

        // Act
        this.paperEditor.GetType()
            .GetMethod(
                "PaperItem_PropertyChanged", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            !.Invoke(
                this.paperEditor,
                new object[] { this.paperItemMock.Object, new PropertyChangedEventArgs(nameof(IPaperItem.IsSelected)) });

        // Assert
        Assert.That(eventTriggered, Is.True, "PaperItemSelected event was not triggered when item was selected");
    }

    [Test]
    public void AddPaperItem_WhenAddedItemImplementsINotifyPropertyChanged_ShouldSubscribeToPropertyChanged()
    {
        // Arrange
        var notifyItemMock = new Mock<IPaperItem>();
        notifyItemMock.As<INotifyPropertyChanged>();
        var itemType = PaperItemType.Text;

        this.paperItemFactoryMock.Setup(f => f.Create(itemType)).Returns(notifyItemMock.Object);

        // Act
        this.paperEditor.AddPaperItem(itemType);

        // Assert
        notifyItemMock.As<INotifyPropertyChanged>()
            .VerifyAdd(i => i.PropertyChanged += It.IsAny<PropertyChangedEventHandler>(), Times.Once);
    }

    [Test]
    public void RemovePaperItem_WhenRemovedItemImplementsINotifyPropertyChanged_ShouldUnsubscribeFromPropertyChanged()
    {
        // Arrange
        var itemId = Guid.NewGuid();
        var notifyItemMock = new Mock<IPaperItem>();
        notifyItemMock.As<INotifyPropertyChanged>();

        this.paperStorageMock.Object.PaperItems.Add(itemId, notifyItemMock.Object);

        // Act
        this.paperEditor.RemovePaperItem(itemId);

        // Assert
        notifyItemMock.As<INotifyPropertyChanged>()
            .VerifyRemove(i => i.PropertyChanged -= It.IsAny<PropertyChangedEventHandler>(), Times.Once);
    }
}
