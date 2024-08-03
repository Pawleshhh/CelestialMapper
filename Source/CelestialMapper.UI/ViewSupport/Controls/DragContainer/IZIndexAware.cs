namespace CelestialMapper.UI;

public interface IZIndexAware
{

    public RelayCommand<ZIndexAction> ZIndexCommand { get; set; }

}
