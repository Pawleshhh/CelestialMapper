namespace CelestialMapper.ViewModel;

public interface IPaperEditor
{

    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemAdded;

    public void AddPaperItem(PaperItemType itemType);

}
