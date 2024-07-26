namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperItemFactory), typeof(PaperItemFactory), IsSingleton = true, Key = nameof(PaperItemFactory))]
public class PaperItemFactory : IPaperItemFactory
{

    private const string ValuePrefix = "String.PaperItem.DefaultValue";

    private readonly IResourceResolver resourceResolver;
    private readonly IIoCManager ioCManager;

    public PaperItemFactory(
        IResourceResolver resourceResolver,
        IIoCManager ioCManager)
    {
        this.resourceResolver = resourceResolver;
        this.ioCManager = ioCManager;
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
        return type switch
        {
            PaperItemType.Map => this.ioCManager.ServiceProvider.ResolveViewModel<MapViewModel>(FeatureNames.Map),
            PaperItemType.Text => new TextItem { Text = (string)value },
            _ => ThrowWhenTypeNotHandled(type)
        };
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
}
