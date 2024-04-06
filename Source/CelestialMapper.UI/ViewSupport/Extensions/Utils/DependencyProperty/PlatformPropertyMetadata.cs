namespace CelestialMapper.UI;

public class PlatformPropertyMetadata<TDepOb, TProperty> : PropertyMetadata
    where TDepOb : DependencyObject
{

    public new TProperty DefaultValue => (TProperty)base.DefaultValue;

    public PlatformPropertyMetadata()
        : base()
    {

    }

    public PlatformPropertyMetadata(TProperty? defaultValue)
        : base(defaultValue)
    {

    }

    public PlatformPropertyMetadata(PropertyChangedCallback<TDepOb, TProperty> propertyChangedCallback)
        : base((s, e) => propertyChangedCallback.Invoke((TDepOb)s, new(e)))
    {

    }

    public PlatformPropertyMetadata(TProperty? defaultValue, PropertyChangedCallback<TDepOb, TProperty> propertyChangedCallback)
        : base(defaultValue, (s, e) => propertyChangedCallback.Invoke((TDepOb)s, new(e)))
    {

    }
}