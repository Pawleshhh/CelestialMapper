using CelestialMapper.ViewModel;
using Moq;

namespace CelestialMapper.UI.Test;

[TestFixture]
public class MenuViewTest : FeatureViewTest<MenuView, MenuViewModel>
{
    public override FeatureName DefaultFeatureName => new("Menu");

    public override Func<IServiceProvider, MenuView> FeatureViewFactory
        => s => new MenuView(ServiceProvider.Object, false);

}