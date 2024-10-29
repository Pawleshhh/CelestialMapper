namespace CelestialMapper.ViewModel;

public interface IPaperStorage
{

    public IDictionary<Guid, IPaperItem> PaperItems { get; }

}
