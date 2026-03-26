
namespace CelestialMapper.UI;

/// <summary>
/// Interaction logic for TimeMachineView.xaml
/// </summary>
public partial class TimeMachineView : FeatureViewBase
{
    public TimeMachineView()
    {
        InitializeView();
    }

    public TimeMachineView(IServiceProvider serviceProvider, bool allowInitializeComponent = true)
        : base(serviceProvider, allowInitializeComponent)
    {
        if (!AllowInitializeComponent)
        {
            return;
        }

        InitializeView();
    }

    public override FeatureName DefaultFeatureName => FeatureNames.TimeMachine;

    protected override Type ViewModelType => typeof(TimeMachineViewModel);
    
    public override void InitializeView()
    {
        InitializeComponent();
    }
}
