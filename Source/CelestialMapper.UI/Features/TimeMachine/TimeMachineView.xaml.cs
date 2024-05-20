
namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for TimeMachineView.xaml
/// </summary>
public partial class TimeMachineView : FeatureViewBase
{
    public TimeMachineView()
    {
        InitializeComponent();
    }

    public TimeMachineView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeComponent();
    }

    public override string DefaultFeatureName => FeatureNames.TimeMachine;

    protected override Type ViewModelType => typeof(TimeMachineViewModel);
}
