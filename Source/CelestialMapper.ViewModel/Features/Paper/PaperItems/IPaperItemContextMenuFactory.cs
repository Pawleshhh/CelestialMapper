
namespace CelestialMapper.ViewModel;

public interface IPaperItemContextMenuFactory
{

    public IEnumerable<UICommand<IPaperItem>> CreateCommands(IPaperItem paperItem);

}