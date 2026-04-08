namespace CelestialMapper.ViewModel;

[Export(typeof(OverlayToolHelper), IsKeyed = false, IsSingleton =true, Key = nameof(OverlayToolHelper))]
public class OverlayToolHelper
{

    private Action<FeatureName?, object?>? setOverlayTool;

    public void Setup(Action<FeatureName?, object?> setOverlayTool)
    {
        this.setOverlayTool = setOverlayTool;
    }

    public void SetActiveOverlayTool(FeatureName? featureName, object? data = null)
    {
        this.setOverlayTool?.Invoke(featureName, data);
    }

}
