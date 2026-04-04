using CelestialMapper.Core.Astronomy;

namespace CelestialMapper.ViewModel;

public class VisualStarData : CelestialObjectVisualData
{

    public VisualStarData(CelestialObject celestialObject)
        : base(celestialObject)
    {
    }

    public PropertyWrapper<double> Magnitude { get; } = new(nameof(Magnitude));

    public PropertyWrapper<string> FillColor { get; } = new("Red", nameof(FillColor));

    public override void InitializeProperties()
    {
        base.InitializeProperties();

        Magnitude.SetupDelegates(
            onBeforeSetValue: null,
            onAfterSetValue: v =>
            {
                var size = CelestialObjectHelper.GetSizeBasedOnMagnitude(v);
                Width.Value = size;
                Height.Value = size;
            });
        
        Properties.AddRange(new IPropertyWrapper[] {
            Magnitude,
            FillColor
        });
    }
}
