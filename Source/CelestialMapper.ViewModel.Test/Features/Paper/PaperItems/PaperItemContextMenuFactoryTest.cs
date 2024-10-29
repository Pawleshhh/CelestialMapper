using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class PaperItemContextMenuFactoryTest
{
    private PaperItemContextMenuFactory contextMenuFactory;
    private Mock<IPaperStorage> mockPaperStorage;
    private Mock<IZIndexProcessor> mockZIndexProcessor;
    private Mock<IPaperItem> mockPaperItem;

    [SetUp]
    public void SetUp()
    {
        this.mockPaperStorage = new Mock<IPaperStorage>();
        this.mockZIndexProcessor = new Mock<IZIndexProcessor>();
        this.contextMenuFactory = new PaperItemContextMenuFactory(this.mockPaperStorage.Object, this.mockZIndexProcessor.Object);
        this.mockPaperItem = new Mock<IPaperItem>();
    }

    [Test]
    public void CreateCommands_ReturnsAllBaseCommands()
    {
        var commands = this.contextMenuFactory.CreateCommands(this.mockPaperItem.Object).ToList();

        Assert.Multiple(() =>
        {
            Assert.That(commands.Count, Is.EqualTo(4));
            Assert.That(commands[0].Id, Is.EqualTo("BringToFront"));
            Assert.That(commands[0].Label, Is.EqualTo("Bring To Front"));
            Assert.That(commands[1].Id, Is.EqualTo("SendToBack"));
            Assert.That(commands[1].Label, Is.EqualTo("Send To Back"));
            Assert.That(commands[2].Id, Is.EqualTo("BringForward"));
            Assert.That(commands[2].Label, Is.EqualTo("Bring Forward"));
            Assert.That(commands[3].Id, Is.EqualTo("SendBackward"));
            Assert.That(commands[3].Label, Is.EqualTo("Send Backward"));
        });
    }

    [Test]
    public void BringToFront_ExecutesCorrectZIndexProcessCommand()
    {
        var commands = this.contextMenuFactory.CreateCommands(this.mockPaperItem.Object).ToList();
        this.mockPaperStorage.Setup(ps => ps.PaperItems.Values).Returns(new List<IPaperItem> { this.mockPaperItem.Object });

        commands.First(c => c.Id == "BringToFront").Execute(this.mockPaperItem.Object);

        this.mockZIndexProcessor
            .Verify(zp => zp.Process(
                    It.IsAny<IEnumerable<IPaperItem>>(),
                    this.mockPaperItem.Object, 
                    ZIndexAction.BringToFront), 
                Times.Once);
    }

    [Test]
    public void SendToBack_ExecutesCorrectZIndexProcessCommand()
    {
        var commands = this.contextMenuFactory
            .CreateCommands(this.mockPaperItem.Object).ToList();
        this.mockPaperStorage
            .Setup(ps => ps.PaperItems.Values).Returns(new List<IPaperItem> { this.mockPaperItem.Object });

        commands.First(c => c.Id == "SendToBack")
            .Execute(this.mockPaperItem.Object);

        this.mockZIndexProcessor
            .Verify(zp => zp.Process(
                    It.IsAny<IEnumerable<IPaperItem>>(),
                    this.mockPaperItem.Object, 
                    ZIndexAction.SendToBack),
                Times.Once);
    }

    [Test]
    public void BringForward_ExecutesCorrectZIndexProcessCommand()
    {
        var commands = this.contextMenuFactory
            .CreateCommands(this.mockPaperItem.Object).ToList();
        this.mockPaperStorage
            .Setup(ps => ps.PaperItems.Values)
            .Returns(new List<IPaperItem> { this.mockPaperItem.Object });

        commands
            .First(c => c.Id == "BringForward")
            .Execute(this.mockPaperItem.Object);

        this.mockZIndexProcessor
            .Verify(zp => zp.Process(
                    It.IsAny<IEnumerable<IPaperItem>>(), 
                    this.mockPaperItem.Object, 
                    ZIndexAction.BringForward), 
                Times.Once);
    }

    [Test]
    public void SendBackward_ExecutesCorrectZIndexProcessCommand()
    {
        var commands = this.contextMenuFactory
            .CreateCommands(this.mockPaperItem.Object).ToList();
        this.mockPaperStorage
            .Setup(ps => ps.PaperItems.Values)
            .Returns(new List<IPaperItem> { this.mockPaperItem.Object });

        commands
            .First(c => c.Id == "SendBackward")
            .Execute(this.mockPaperItem.Object);

        this.mockZIndexProcessor
            .Verify(zp => zp.Process(
                    It.IsAny<IEnumerable<IPaperItem>>(),
                    this.mockPaperItem.Object, 
                    ZIndexAction.SendBackward), 
                Times.Once);
    }

    [Test]
    public void CommandExecution_WithNullItem_DoesNotCallProcess()
    {
        var commands = this.contextMenuFactory.CreateCommands(null!).ToList();
        this.mockPaperStorage
            .Setup(ps => ps.PaperItems.Values)
            .Returns(new List<IPaperItem> { this.mockPaperItem.Object });

        foreach (var command in commands)
        {
            command.Execute(null);
        }

        this.mockZIndexProcessor
            .Verify(zp => zp.Process(
                    It.IsAny<IEnumerable<IPaperItem>>(), 
                    It.IsAny<IPaperItem>(), 
                    It.IsAny<ZIndexAction>()),
                Times.Never);
    }
}
