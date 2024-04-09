using CelestialMapper.ViewModel;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class TimeMachineViewTest : FeatureViewTest<TimeMachineView, TimeMachineViewModel>
{

    public override string DefaultFeatureName => "TimeMachine";

    public override Func<IServiceProvider, TimeMachineView> FeatureViewFactory
        => s => new TimeMachineView(ServiceProvider.Object, false);

}
