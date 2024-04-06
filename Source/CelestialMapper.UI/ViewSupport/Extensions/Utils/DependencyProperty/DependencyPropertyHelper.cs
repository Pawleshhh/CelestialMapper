namespace CelestialMapper.UI;

public static class DependencyPropertyHelper
{

    public static DependencyProperty Register<TProperty, TOwner>(string name)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner));

    public static DependencyProperty Register<TProperty, TOwner>(string name, TProperty? defaultValue)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), new(defaultValue));

    public static DependencyProperty Register<TProperty, TOwner>(string name, PropertyChangedCallback<TOwner, TProperty> callback)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), new((s, e) => callback.Invoke((TOwner)s, new(e))));

    public static DependencyProperty Register<TProperty, TOwner>(string name, TProperty? defaultValue, PropertyChangedCallback<TOwner, TProperty> callback)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), new(defaultValue, (s, e) => callback.Invoke((TOwner)s, new(e))));

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner));

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, TProperty? defaultValue)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), new(defaultValue));

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, PropertyChangedCallback<DependencyObject, TProperty> callback)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), new((s, e) => callback.Invoke(s, new(e))));

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, TProperty? defaultValue, PropertyChangedCallback<DependencyObject, TProperty> callback)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), new(defaultValue, (s, e) => callback.Invoke(s, new(e))));

    public static DependencyProperty RegisterAttached<TProperty, TOwner, TDepObj>(string name, PropertyChangedCallback<TDepObj, TProperty> callback)
        where TDepObj : DependencyObject
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), new((s, e) => callback.Invoke((TDepObj)s, new(e))));

    public static DependencyProperty RegisterAttached<TProperty, TOwner, TDepObj>(string name, TProperty? defaultValue, PropertyChangedCallback<TDepObj, TProperty> callback)
        where TDepObj : DependencyObject
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), new(defaultValue, (s, e) => callback.Invoke((TDepObj)s, new(e))));

    public static T GetValue<T>(this DependencyObject @this, DependencyProperty dp)
        => (T)@this.GetValue(dp);

}
