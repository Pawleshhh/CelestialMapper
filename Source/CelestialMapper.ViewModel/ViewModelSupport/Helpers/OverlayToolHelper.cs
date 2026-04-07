namespace CelestialMapper.ViewModel;

[Export(typeof(OverlayToolHelper), IsKeyed = false, IsSingleton =true, Key = nameof(OverlayToolHelper))]
public class OverlayToolHelper
{

    private Action<FeatureName?>? setOverlayTool;

    public void Setup(Action<FeatureName?> setOverlayTool)
    {
        this.setOverlayTool = setOverlayTool;
    }

    public void SetActiveOverlayTool(FeatureName? featureName)
    {
        this.setOverlayTool?.Invoke(featureName);
    }

}
