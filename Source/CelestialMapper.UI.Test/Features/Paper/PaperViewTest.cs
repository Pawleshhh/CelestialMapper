using CelestialMapper.ViewModel;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class PaperViewTest : FeatureViewTest<PaperView, PaperViewModel>
{
    public override FeatureName DefaultFeatureName => new("Paper");

    public override Func<IServiceProvider, PaperView> FeatureViewFactory
        => s => new PaperView(ServiceProvider.Object, false);
}
