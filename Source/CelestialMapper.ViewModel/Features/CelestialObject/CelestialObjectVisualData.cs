using CelestialMapper.Core.Astronomy;

namespace CelestialMapper.ViewModel;

public abstract class CelestialObjectVisualData : VisualDataBase
{
    public object Data { get; }

    public CelestialObjectVisualData(object data)
    {
        Data = data;
    }

    public override void InitializeProperties()
    {
        base.InitializeProperties();
        X.IsReadOnly = true;
        Y.IsReadOnly = true;
        Width.IsReadOnly = true;
        Height.IsReadOnly = true;

        SubscribeToProperties();
    }

}
