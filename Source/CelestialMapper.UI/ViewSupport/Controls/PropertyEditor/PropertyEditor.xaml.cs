namespace CelestialMapper.UI;

using CelestialMapper.Core.Astronomy;
using System;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;
using static CelestialMapper.UI.DependencyPropertyHelper;

/// <summary>
/// Interaction logic for MainLayout.xaml
/// </summary>
public partial class PropertyEditor : PlatformUserControl
{
    public PropertyEditor()
    {
        InitializeComponent();
    }

    public IList Properties
    {
        get { return (IList)GetValue(PropertiesProperty); }
        set { SetValue(PropertiesProperty, value); }
    }

    public static readonly DependencyProperty PropertiesProperty =
        Register(nameof(Properties), new PlatformPropertyMetadata<PropertyEditor, IList>());

}
