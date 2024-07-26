namespace CelestialMapper.ViewModel;

public interface IPaperItem : IVisualData
{
    public int Id { get; }

    public PaperItemType ItemType { get; }
}

public abstract class PaperItemBase : VisualDataBase, IPaperItem
{
    public PaperItemBase()
    {
    }

    public int Id
    {
        get => GetPropertyValue<int>();
        init => SetPropertyValue(value);
    }

    public abstract PaperItemType ItemType { get; }
}

public abstract class PaperItemBaseViewModel : VisualDataViewModelBase, IPaperItem
{
    public PaperItemBaseViewModel(IViewModelSupport viewModelSupport)
        : base(viewModelSupport)
    {
    }

    public int Id
    {
        get => GetPropertyValue<int>();
        init => SetPropertyValue(value);
    }

    public abstract PaperItemType ItemType { get; }
}
