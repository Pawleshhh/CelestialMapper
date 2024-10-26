namespace CelestialMapper.ViewModel;

public interface IPaperEditor
{

    public IDictionary<Guid, IPaperItem> PaperItems { get; }

    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemAdded;
    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemRemoved;
    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemSelected;

    public void AddPaperItem(PaperItemType itemType);
    public void AddPaperItem(PaperItemType itemType, object value);
    public void RemovePaperItem(Guid guid);
}
