using CelestialMapper.Core.Astronomy;

namespace CelestialMapper.ViewModel;

public class VisualStarData : CelestialObjectVisualData
{

    public VisualStarData(CelestialObject celestialObject)
        : base(celestialObject)
    {
    }

    public PropertyWrapper<double> Magnitude { get; } = new(nameof(Magnitude));

    public override void InitializeProperties()
    {
        base.InitializeProperties();

        Magnitude.SetupDelegates(
            onBeforeSetValue: null,
            onAfterSetValue: val =>
            {
                var size = CelestialObjectHelper.GetSizeBasedOnMagnitude(val);
                Width.Value = size;
                Height.Value = size;
            });
        
        Properties.AddRange(new IPropertyWrapper[] {
            Magnitude
        });

        Magnitude.Value = ((CelestialObject)Data).Magnitude;
        BackgroundColor.Value = "Orange";

        SubscribeToProperties();
    }
}
