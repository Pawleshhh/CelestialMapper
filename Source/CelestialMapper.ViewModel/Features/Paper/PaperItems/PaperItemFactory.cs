namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperItemFactory), typeof(PaperItemFactory), IsSingleton = true, Key = nameof(PaperItemFactory))]
public class PaperItemFactory : IPaperItemFactory
{

    private const string ValuePrefix = "String.PaperItem.DefaultValue";

    private readonly IPaperItemContextMenuFactory contextMenuFactory;
    private readonly IResourceResolver resourceResolver;
    private readonly IIoCManager ioCManager;
    private readonly IFontService fontService;

    public PaperItemFactory(
        IPaperItemContextMenuFactory contextMenuFactory,
        IResourceResolver resourceResolver,
        IIoCManager ioCManager,
        IFontService fontService)
    {
        this.contextMenuFactory = contextMenuFactory;
        this.resourceResolver = resourceResolver;
        this.ioCManager = ioCManager;
        this.fontService = fontService;
    }

    public IPaperItem Create(PaperItemType type)
    {
        return type switch
        {
            PaperItemType.Map => Create(type, null!),
            PaperItemType.Text => Create(type, GetResourceString($"{ValuePrefix}.Text")),
            _ => ThrowWhenTypeNotHandled(type)
        };
    }

    public IPaperItem Create(PaperItemType type, object value)
    {
        var item = type switch
        {
            PaperItemType.Map => this.ioCManager.ServiceProvider.ResolveViewModel<MapViewModel>(FeatureNames.Map),
            PaperItemType.Text => GetTextItem(value),
            _ => ThrowWhenTypeNotHandled(type)
        };

        item.Commands = new(this.contextMenuFactory.CreateCommands(item));
        return item;
    }

    private IPaperItem ThrowWhenTypeNotHandled(PaperItemType type)
    {
        throw new InvalidOperationException($"Not expected type of paper item - {type}");
    }

    private string GetResourceString(string key)
    {
        this.resourceResolver.TryResolveString(key, out var value);
        return value;
    }

    private TextItem GetTextItem(object value)
    {
        var textItem = new TextItem(this.fontService)
        {
            Id = Guid.NewGuid()
        };
        textItem.InitializeProperties();
        textItem.Text.Value = value as string;
        return textItem;
    }
}
