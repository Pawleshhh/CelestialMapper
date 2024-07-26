
namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperEditor), typeof(PaperEditor), IsKeyed = false, IsSingleton = true, Key = nameof(PaperEditor))]
public class PaperEditor : IPaperEditor
{

    public PaperEditor()
    {
        PaperItemAdded += (s, e) => { };   
    }

    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemAdded;

    public void AddPaperItem(PaperItemType itemType)
    {
        throw new NotImplementedException();
    }
}
