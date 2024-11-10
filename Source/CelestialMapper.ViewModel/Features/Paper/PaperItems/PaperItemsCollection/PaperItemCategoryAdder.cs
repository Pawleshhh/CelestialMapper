using System.Collections.ObjectModel;

namespace CelestialMapper.ViewModel;

public class PaperItemCategoryAdder : NotifyPropertyChangedBase
{

    protected readonly IPaperEditor paperEditor;

    public PaperItemCategoryAdder(
        IPaperEditor paperEditor)
    {
        this.paperEditor = paperEditor;
    }

    public PaperItemCatergory Category { get; set; }

    public ObservableCollection<PaperItemAdder> PaperItemAdders { get; } = new();

}

public class PaperItemAdder : NotifyPropertyChangedBase
{

    protected IPaperEditor paperEditor;

    public PaperItemAdder(
        IPaperEditor paperEditor)
    {
        this.paperEditor= paperEditor;
        CreateCommand = new UICommand(Create);
    }

    public required PaperItemType PaperItemType { get; init; }

    public UICommand CreateCommand { get; }

    public void Create(object? o)
    {
        this.paperEditor.AddPaperItem(PaperItemType);
    }
}