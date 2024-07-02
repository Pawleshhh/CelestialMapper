using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class ExportMenuTest : FeatureViewTest<ExportMenuView, ExportMenuViewModel>
{
    public override FeatureName DefaultFeatureName => new("ExportMenu");

    public override Func<IServiceProvider, ExportMenuView> FeatureViewFactory
        => s => new ExportMenuView(ServiceProvider.Object, false);

}