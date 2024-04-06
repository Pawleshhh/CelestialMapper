using System.Windows;

namespace CelestialMapper.UI;

public static class DependencyPropertyHelper
{

    #region Register

    public static DependencyProperty Register<TProperty, TOwner>(string name)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner));

    public static DependencyProperty Register<TProperty, TOwner>(string name, PlatformPropertyMetadata<TOwner, TProperty> propertyMetadata)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), propertyMetadata);

    public static DependencyProperty Register<TProperty, TOwner>(string name, PlatformFrameworkPropertyMetadata<TOwner, TProperty> frameworkPropertyMetadata)
        where TOwner : DependencyObject
        => DependencyProperty.Register(name, typeof(TProperty), typeof(TOwner), frameworkPropertyMetadata);

    #endregion

    #region RegisterAttached

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner));

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, PlatformPropertyMetadata<DependencyObject, TProperty> propertyMetadata)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), propertyMetadata);

    public static DependencyProperty RegisterAttached<TProperty, TOwner, TDepObj>(string name, PlatformPropertyMetadata<TDepObj, TProperty> propertyMetadata)
        where TDepObj : DependencyObject
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), propertyMetadata);

    public static DependencyProperty RegisterAttached<TProperty, TOwner>(string name, PlatformFrameworkPropertyMetadata<DependencyObject, TProperty> frameworkPropertyMetadata)
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), frameworkPropertyMetadata);

    public static DependencyProperty RegisterAttached<TProperty, TOwner, TDepObj>(string name, PlatformFrameworkPropertyMetadata<TDepObj, TProperty> frameworkPropertyMetadata)
        where TDepObj : DependencyObject
        => DependencyProperty.RegisterAttached(name, typeof(TProperty), typeof(TOwner), frameworkPropertyMetadata);

    #endregion

    public static T GetValue<T>(this DependencyObject @this, DependencyProperty dp)
        => (T)@this.GetValue(dp);

}
