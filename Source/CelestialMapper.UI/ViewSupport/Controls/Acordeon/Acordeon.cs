namespace CelestialMapper.UI;

using CelestialMapper.Common;
using static CelestialMapper.UI.DependencyPropertyHelper;

public class Acordeon : PlatformUserControl
{

    public static readonly string AcordeonDefaultStyleKey = "Style.Acordeon";

    #region Constructors

    public Acordeon()
    {
        Style = TryFindResource(AcordeonDefaultStyleKey) as Style;
    }

    public Acordeon(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        Style = TryFindResource(AcordeonDefaultStyleKey) as Style;
    }

    #endregion

    #region Properties

    public IList ItemsSource
    {
        get { return (IList)GetValue(ItemsSourceProperty); }
        set { SetValue(ItemsSourceProperty, value); }
    }

    public static readonly DependencyProperty ItemsSourceProperty =
        Register(nameof(ItemsSource) , new PlatformPropertyMetadata<Acordeon, IList>(Array.Empty<object>()));

    #endregion

}
