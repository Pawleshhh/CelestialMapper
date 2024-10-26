
namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperStorage), typeof(PaperStorage), IsSingleton = true, Key = nameof(PaperStorage))]
public class PaperStorage : IPaperStorage
{
    public IDictionary<Guid, IPaperItem> PaperItems { get; } = new Dictionary<Guid, IPaperItem>();

}
