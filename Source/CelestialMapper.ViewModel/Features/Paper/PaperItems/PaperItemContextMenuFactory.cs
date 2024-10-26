namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperItemContextMenuFactory), typeof(PaperItemContextMenuFactory), IsSingleton = true, Key = nameof(PaperItemContextMenuFactory))]
public class PaperItemContextMenuFactory : IPaperItemContextMenuFactory
{

    private readonly IPaperStorage paperStorage;
    private readonly IZIndexProcessor zIndexProcess;

    public PaperItemContextMenuFactory(
        IPaperStorage paperStorage,
        IZIndexProcessor zIndexProcessor)
    {
        this.paperStorage = paperStorage;
        this.zIndexProcess = zIndexProcessor;
    }

    public IEnumerable<UICommand<IPaperItem>> CreateCommands(IPaperItem item)
    {
        var baseCommands = BaseCommands(item);

        //var typeCommands = item.ItemType switch
        //{
        //};

        return baseCommands;
    }

    private IEnumerable<UICommand<IPaperItem>> BaseCommands(IPaperItem item)
    {
        yield return new UICommand<IPaperItem>(_ => BringToFront(item))
        {
            Id = nameof(BringToFront),
            Label = "Bring To Front"
        };
        yield return new UICommand<IPaperItem>(_ => SendToBack(item))
        {
            Id = nameof(SendToBack),
            Label = "Send To Back"
        };
        yield return new UICommand<IPaperItem>(_ => BringForward(item))
        {
            Id = nameof(BringForward),
            Label = "Bring Forward"
        };
        yield return new UICommand<IPaperItem>(_ => SendBackward(item))
        {
            Id = nameof(SendBackward),
            Label = "Send Backward"
        };
    }

    private void BringToFront(IPaperItem? item)
    {
        if (item is null)
        {
            return;
        }

        this.zIndexProcess.Process(this.paperStorage.PaperItems.Values, item, ZIndexAction.BringToFront);
    }

    private void SendToBack(IPaperItem? item)
    {
        if (item is null)
        {
            return;
        }

        this.zIndexProcess.Process(this.paperStorage.PaperItems.Values, item, ZIndexAction.SendToBack);
    }

    private void BringForward(IPaperItem? item)
    {
        if (item is null)
        {
            return;
        }

        this.zIndexProcess.Process(this.paperStorage.PaperItems.Values, item, ZIndexAction.BringForward);
    }

    private void SendBackward(IPaperItem? item)
    {
        if (item is null)
        {
            return;
        }

        this.zIndexProcess.Process(this.paperStorage.PaperItems.Values, item, ZIndexAction.SendBackward);
    }
}
