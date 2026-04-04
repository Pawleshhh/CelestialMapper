using CelestialMapper.Core.Astronomy;

namespace CelestialMapper.ViewModel;

public class CelestialObjectVisualData : VisualDataBase
{
    public CelestialObject CelestialObject { get; }

    public CelestialObjectVisualData(CelestialObject celestialObject)
    {
        CelestialObject = celestialObject;
    }

    public override void InitializeProperties()
    {
        base.InitializeProperties();
        X.IsReadOnly = true;
        Y.IsReadOnly = true;
        Width.IsReadOnly = true;
        Height.IsReadOnly = true;
    }


}
