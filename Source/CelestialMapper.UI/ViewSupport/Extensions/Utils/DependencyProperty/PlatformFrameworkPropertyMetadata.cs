namespace CelestialMapper.UI;

public class PlatformFrameworkPropertyMetadata<TDepObj, TProperty> : FrameworkPropertyMetadata
    where TDepObj : DependencyObject
{

    public new TProperty DefaultValue => (TProperty)base.DefaultValue;

    public PlatformFrameworkPropertyMetadata()
        : base()
    {

    }

    public PlatformFrameworkPropertyMetadata(TProperty? defaultValue)
        : base(defaultValue)
    {

    }

    public PlatformFrameworkPropertyMetadata(PropertyChangedCallback<TDepObj, TProperty> propertyChangedCallback)
        : base((s, e) => propertyChangedCallback.Invoke((TDepObj)s, new(e)))
    {

    }

    public PlatformFrameworkPropertyMetadata(TProperty? defaultValue, FrameworkPropertyMetadataOptions flags)
        : base(defaultValue, flags)
    {

    }

    public PlatformFrameworkPropertyMetadata(TProperty? defaultValue, PropertyChangedCallback<TDepObj, TProperty> propertyChangedCallback)
        : base(defaultValue, (s, e) => propertyChangedCallback.Invoke((TDepObj)s, new(e)))
    {

    }

    public PlatformFrameworkPropertyMetadata(TProperty? defaultValue, FrameworkPropertyMetadataOptions flags, PropertyChangedCallback<TDepObj, TProperty> propertyChangedCallback)
        : base(defaultValue, flags, (s, e) => propertyChangedCallback.Invoke((TDepObj)s, new(e)))
    {

    }
}
