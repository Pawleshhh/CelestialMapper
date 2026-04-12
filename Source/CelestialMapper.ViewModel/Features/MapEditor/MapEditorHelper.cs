namespace CelestialMapper.ViewModel;

[Export(typeof(MapEditorHelper), IsKeyed = false, IsSingleton = true, Key = nameof(MapEditorHelper))]
public class MapEditorHelper
{

    private MapViewModel? mapToEdit;

    public MapViewModel? MapToEdit
    {
        get => this.mapToEdit;
        set
        {
            this.mapToEdit = value;
            MapToEditChanged?.Invoke(this, new(value));
        }
    }

    public event PlatformEventHandler<MapEditorHelper, PlatformEventArgs<MapViewModel?>>? MapToEditChanged;

}
