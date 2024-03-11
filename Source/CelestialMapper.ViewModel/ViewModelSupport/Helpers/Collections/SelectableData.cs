using System.Collections.ObjectModel;
using System.ComponentModel;

namespace CelestialMapper.ViewModel;

public interface ISelectableData : INotifyPropertyChanged
{

    public bool FirstItemSelectedByDefault { get; set; }

    public object? Selected { get; set; }

    public IList Items { get; }

    public void UpdateItems(IList items);

}

public interface ISelectableData<T> : ISelectableData
{

    object? ISelectableData.Selected
    {
        get => Selected;
        set => Selected = (T?)value;
    }
    public new T? Selected { get; set; }

    IList ISelectableData.Items => Items;
    public new ObservableCollection<T> Items { get; }

    void ISelectableData.UpdateItems(IList items)
    {
        Items.Reset(items.Cast<T>());
    }

    public void UpdateItems(IEnumerable<T> items);

}

public class SelectableData<T> : NotifyPropertyChangedBase, ISelectableData<T>
{

    public SelectableData() 
    {
    }

    public SelectableData(IEnumerable<T> items)
    {
        Items.AddRange(items);
    }

    public bool FirstItemSelectedByDefault
    {
        get => GetPropertyValue<bool>();
        set => SetPropertyValue(value);
    }

    public T? Selected
    {
        get => GetPropertyValue<T>();
        set => SetPropertyValue(value);
    }

    public ObservableCollection<T> Items { get; } = new();

    public void UpdateItems(IEnumerable<T> items)
    {
        Items.Reset(items);

        if (FirstItemSelectedByDefault && Items.Any())
        {
            Selected = Items.First();
        }

        RisePropertyChanged(nameof(Items));
    }

}