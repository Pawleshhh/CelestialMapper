using System.ComponentModel;
using CelestialMapper.Common;

namespace CelestialMapper.ViewModel;

[Export(typeof(IPaperEditor), typeof(PaperEditor), IsKeyed = false, IsSingleton = true, Key = nameof(PaperEditor))]
public class PaperEditor : IPaperEditor
{

    private readonly IPaperItemFactory paperItemFactory;

    public PaperEditor(IPaperItemFactory paperItemFactory)
    {
        this.paperItemFactory = paperItemFactory;

        PaperItemAdded += (s, e) => { };
        PaperItemRemoved += (s, e) => { };
        PaperItemSelected += (s, e) => { };
    }

    public IDictionary<Guid, IPaperItem> PaperItems { get; } = new Dictionary<Guid, IPaperItem>();

    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemAdded;
    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemRemoved;
    public event PlatformEventHandler<IPaperEditor, PlatformEventArgs<IPaperItem>> PaperItemSelected;

    protected virtual void OnPaperItemAdded(IPaperItem item)
    {
        PaperItemAdded(this, new(item));

        if (item is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged += PaperItem_PropertyChanged;
        }
    }

    protected virtual void OnPaperItemRemoved(IPaperItem item)
    {
        PaperItemRemoved(this, new(item));

        if (item is INotifyPropertyChanged notifyPropertyChanged)
        {
            notifyPropertyChanged.PropertyChanged -= PaperItem_PropertyChanged;
        }
    }

    protected virtual void OnPaperItemSelected(IPaperItem item)
    {
        PaperItemSelected(this, new(item));
    }

    public void AddPaperItem(PaperItemType itemType)
    {
        var item = this.paperItemFactory.Create(itemType);
        PaperItems.Add(new(item.Id, item));

        OnPaperItemAdded(item);
    }

    public void AddPaperItem(PaperItemType itemType, object value)
    {
        var item = this.paperItemFactory.Create(itemType, value);
        PaperItems.Add(new(item.Id, item));

        OnPaperItemAdded(item);
    }

    public void RemovePaperItem(Guid guid)
    {
        PaperItems.TryGetValue(guid, out var item);
        if (item is not null)
        {
            OnPaperItemRemoved(item);
        }
    }

    private void PaperItem_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not IPaperItem item)
        {
            return;
        }
        if (e.PropertyName != nameof(IPaperItem.IsSelected))
        {
            return;
        }

        if (item.IsSelected)
        {
            PaperItems
                .Where(x => x.Key != item.Id && x.Value.IsSelected)
                .ForEach(x => x.Value.IsSelected = false);
            OnPaperItemSelected(item);
        }
    }
}
