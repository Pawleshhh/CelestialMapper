namespace CelestialMapper.UI;

public class CelestialUIObjectSelectionHelper
{

    private readonly Action onSelectionChanged;

    private readonly HashSet<ICelestialUIObject> selectedObjects = new();

    public CelestialUIObjectSelectionHelper(Action onSelectionChanged)
    {
        this.onSelectionChanged = onSelectionChanged;
    }

    public bool IsMultipleSelectionActive { get; set; }

    public IReadOnlySet<ICelestialUIObject> SelectedObjects => this.selectedObjects;

    public ICelestialUIObject? SelectedObject
    {
        get => SelectedObjects.FirstOrDefault();
        set
        {
            if (value is null)
            {
                this.selectedObjects.Clear();
                this.onSelectionChanged();
                return;
            }

            if (!IsMultipleSelectionActive)
            {
                this.selectedObjects.Clear();
            }

            this.selectedObjects.Add(value);
            this.onSelectionChanged();
        }
    }

    public void AddMultipleSelections(params ICelestialUIObject[] celestialUIObjects)
    {
        foreach (var obj in celestialUIObjects)
        {
            this.selectedObjects.Add(obj);
        }

        this.onSelectionChanged();
    }

}
