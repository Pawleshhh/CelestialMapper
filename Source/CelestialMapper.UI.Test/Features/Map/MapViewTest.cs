using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

[TestFixture]
//[Ignore("Need to find a solution how to mock the resource service (CelestialMap UserControl uses one)")]
public class MapViewTest : FeatureViewTest<MapView, MapViewModel>
{
    public override string DefaultFeatureName => "Map";

    public override Func<IServiceProvider, MapView> FeatureViewFactory
        => s => new MapView(ServiceProvider.Object, false);

    public override void OneTimeSetUp()
    {
        base.OneTimeSetUp();

        ResourceResolver
            .Setup(x => x.ResolveResource("Double.Map.Diameter"))
            .Returns(100);
    }
}
