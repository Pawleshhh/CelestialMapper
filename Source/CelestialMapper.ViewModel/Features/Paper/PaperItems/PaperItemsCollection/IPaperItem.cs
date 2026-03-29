using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

public interface IPaperItem : IVisualData
{
    public Guid Id { get; }

    public PaperItemType ItemType { get; }

    public bool IsSelected { get; set; }

    public ObservableCollection<UICommand<IPaperItem>> Commands { get; set; }

}

public abstract class PaperItemBase : VisualDataBase, IPaperItem
{
    private ObservableCollection<IPropertyWrapper>? properties;

    public PaperItemBase()
    {
        InitializeProperties();
    }

    public ObservableCollection<IPropertyWrapper> Properties
    {
        get => this.properties ??= new();
        set => this.properties = value;
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

    public ObservableCollection<UICommand<IPaperItem>> Commands
    {
        get => GetPropertyValue<ObservableCollection<UICommand<IPaperItem>>>() ?? new();
        set => SetPropertyValue(value);
    }

    protected virtual void InitializeProperties()
    {
        this.Properties.Clear();
    }
}

public abstract class PaperItemBaseViewModel : VisualDataViewModelBase, IPaperItem
{
    private ObservableCollection<IPropertyWrapper>? properties;

    public PaperItemBaseViewModel(IViewModelSupport viewModelSupport)
        : base(viewModelSupport)
    {
        InitializeProperties();
    }

    public ObservableCollection<IPropertyWrapper> Properties
    {
        get => this.properties ??= new();
        set => this.properties = value;
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

    public ObservableCollection<UICommand<IPaperItem>> Commands
    {
        get => GetPropertyValue<ObservableCollection<UICommand<IPaperItem>>>() ?? new();
        set => SetPropertyValue(value);
    }

    protected virtual void InitializeProperties()
    {
        this.Properties.Clear();
    }
}
