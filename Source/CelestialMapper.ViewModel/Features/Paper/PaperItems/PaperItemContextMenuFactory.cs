namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperItemContextMenuFactory), typeof(PaperItemContextMenuFactory), IsSingleton = true, Key = nameof(PaperItemContextMenuFactory))]
public class PaperItemContextMenuFactory : IPaperItemContextMenuFactory
{

    private readonly IZIndexProcessor zIndexProcess;

    public PaperItemContextMenuFactory(IZIndexProcessor zIndexProcessor)
    {
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
        ;
    }

    private void SendToBack(IPaperItem? item)
    {
        ;
    }

    private void BringForward(IPaperItem? item)
    {
        ;
    }

    private void SendBackward(IPaperItem? item)
    {
        ;
    }
}
