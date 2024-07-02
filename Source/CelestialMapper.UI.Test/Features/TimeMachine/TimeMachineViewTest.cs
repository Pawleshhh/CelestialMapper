using CelestialMapper.ViewModel;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class TimeMachineViewTest : FeatureViewTest<TimeMachineView, TimeMachineViewModel>
{

    public override FeatureName DefaultFeatureName => new("TimeMachine");

    public override Func<IServiceProvider, TimeMachineView> FeatureViewFactory
        => s => new TimeMachineView(ServiceProvider.Object, false);

}
