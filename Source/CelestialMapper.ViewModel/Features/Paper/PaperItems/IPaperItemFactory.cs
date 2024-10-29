namespace CelestialMapper.ViewModel;

public interface IPaperItemFactory
{

    public IPaperItem Create(PaperItemType type);

    public IPaperItem Create(PaperItemType type, object value);

}
