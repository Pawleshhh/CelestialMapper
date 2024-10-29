using Moq;

namespace CelestialMapper.ViewModel.Test;

[TestFixture]
public class ZIndexProcessorTest
{
    private ZIndexProcessor zIndexProcessor;
    private Mock<IPaperItem> sourceItem;
    private List<Mock<IPaperItem>> paperItems;

    [SetUp]
    public void Setup()
    {
        this.zIndexProcessor = new ZIndexProcessor();
        this.sourceItem = new Mock<IPaperItem>();
        this.sourceItem.SetupProperty(x => x.ZIndex);
        this.paperItems = new List<Mock<IPaperItem>>();
    }

    private void SetupPaperItems(params int[] zIndices)
    {
        this.paperItems.Clear();
        foreach (var zIndex in zIndices)
        {
            var item = new Mock<IPaperItem>();
            item.SetupProperty(x => x.ZIndex, zIndex);
            this.paperItems.Add(item);
        }
    }

    [Test]
    public void Process_BringToFront_SetsSourceZIndexToMaxPlusOne()
    {
        SetupPaperItems(1, 2, 3, 4);
        this.sourceItem.Object.ZIndex = 2;

        this.zIndexProcessor
            .Process(
                this.paperItems.Select(x => x.Object), 
                this.sourceItem.Object, ZIndexAction.BringToFront);

        Assert.That(this.sourceItem.Object.ZIndex, Is.EqualTo(5));
    }

    [Test]
    public void Process_SendToBack_SetsSourceZIndexToMinMinusOne()
    {
        SetupPaperItems(1, 2, 3, 4);
        this.sourceItem.Object.ZIndex = 3;

        this.zIndexProcessor
            .Process(
                this.paperItems.Select(x => x.Object),
                this.sourceItem.Object, 
                ZIndexAction.SendToBack);

        Assert.That(this.sourceItem.Object.ZIndex, Is.EqualTo(0));
    }

    [Test]
    public void Process_BringForward_IncrementsSourceZIndexByOne()
    {
        SetupPaperItems(1, 2, 3, 4);
        this.sourceItem.Object.ZIndex = 2;

        this.zIndexProcessor
            .Process(
                this.paperItems.Select(x => x.Object),
                this.sourceItem.Object,
                ZIndexAction.BringForward);

        Assert.Multiple(() =>
        {
            Assert.That(this.sourceItem.Object.ZIndex, Is.EqualTo(3));
            Assert.That(this.paperItems[2].Object.ZIndex, Is.EqualTo(2));
        });
    }

    [Test]
    public void Process_SendBackward_DecrementsSourceZIndexByOne()
    {
        SetupPaperItems(1, 2, 3, 4);
        this.sourceItem.Object.ZIndex = 3;

        this.zIndexProcessor
            .Process(
                this.paperItems.Select(x => x.Object),
                this.sourceItem.Object,
                ZIndexAction.SendBackward);

        Assert.Multiple(() =>
        {
            Assert.That(this.sourceItem.Object.ZIndex, Is.EqualTo(2));
            Assert.That(this.paperItems[1].Object.ZIndex, Is.EqualTo(3));
        });
    }

    [Test]
    public void ProcessNewItem_SetsNewItemZIndexToMaxPlusOne()
    {
        SetupPaperItems(1, 2, 3, 4);
        var newItem = new Mock<IPaperItem>();
        newItem.SetupProperty(x => x.ZIndex);

        this.zIndexProcessor.ProcessNewItem(
            this.paperItems.Select(x => x.Object), 
            newItem.Object);

        Assert.That(newItem.Object.ZIndex, Is.EqualTo(5));
    }

    [Test]
    public void ProcessNewItem_WithEmptyItems_SetsNewItemZIndexToZero()
    {
        var newItem = new Mock<IPaperItem>();
        newItem.SetupProperty(x => x.ZIndex);

        this.zIndexProcessor
            .ProcessNewItem(new List<IPaperItem>(), newItem.Object);

        Assert.That(newItem.Object.ZIndex, Is.EqualTo(0));
    }

    [Test]
    public void Process_InvalidAction_ThrowsInvalidOperationException()
    {
        SetupPaperItems(1, 2, 3);
        this.sourceItem.Object.ZIndex = 2;

        Assert.That(() => this.zIndexProcessor.Process(
                    this.paperItems.Select(x => x.Object),
                    this.sourceItem.Object, 
                    (ZIndexAction)99),
            Throws.InvalidOperationException.With.Message.EqualTo("Unexpected value of ZIndexAction: 99"));
    }
}
