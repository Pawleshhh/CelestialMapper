namespace CelestialMapper.ViewModel;

public interface IPaperItem : IVisualData
{
    public Guid Id { get; }

    public PaperItemType ItemType { get; }

    public bool IsSelected { get; set; }

}

public abstract class PaperItemBase : VisualDataBase, IPaperItem
{
    public PaperItemBase()
    {
    }

    public required Guid Id
    {
        get => GetPropertyValue<Guid>();
        init => SetPropertyValue(value);
    }

    public abstract PaperItemType ItemType { get; }

    public bool IsSelected
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public int ZIndex
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }
}

public abstract class PaperItemBaseViewModel : VisualDataViewModelBase, IPaperItem
{
    public PaperItemBaseViewModel(IViewModelSupport viewModelSupport)
        : base(viewModelSupport)
    {
    }

    public Guid Id
    {
        get => GetPropertyValue<Guid>();
        init => SetPropertyValue(value);
    }

    public abstract PaperItemType ItemType { get; }

    public bool IsSelected
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public int ZIndex
    {
        get => GetPropertyValue<int>();
        set => SetPropertyValue(value);
    }
}
