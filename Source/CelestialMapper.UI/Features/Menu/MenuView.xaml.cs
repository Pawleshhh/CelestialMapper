﻿namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for MapView.xaml
/// </summary>
public partial class MenuView : FeatureViewBase
{

    public MenuView()
    {
        InitializeComponent();
    }

    public MenuView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.PropertiesMenu;

    protected override Type ViewModelType => typeof(MenuViewModel);

}
