namespace CelestialMapper.UI;

public static class Validation
{

    #region Error

    public static bool GetIsError(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsErrorProperty);
    }

    public static void SetIsError(DependencyObject obj, bool value)
    {
        obj.SetValue(IsErrorProperty, value);
    }

    public static readonly DependencyProperty IsErrorProperty =
        DependencyProperty.RegisterAttached("IsError", typeof(bool), typeof(Validation), new PropertyMetadata(false));


    public static object GetErrorContent(DependencyObject obj)
    {
        return (object)obj.GetValue(ErrorContentProperty);
    }

    public static void SetErrorContent(DependencyObject obj, object value)
    {
        obj.SetValue(ErrorContentProperty, value);
    }

    public static readonly DependencyProperty ErrorContentProperty =
        DependencyProperty.RegisterAttached("ErrorContent", typeof(object), typeof(Validation), new PropertyMetadata(null));

    #endregion

    #region Success

    public static bool GetIsSuccess(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsSuccessProperty);
    }

    public static void SetIsSuccess(DependencyObject obj, bool value)
    {
        obj.SetValue(IsSuccessProperty, value);
    }

    public static readonly DependencyProperty IsSuccessProperty =
        DependencyProperty.RegisterAttached("IsSuccess", typeof(bool), typeof(Validation), new PropertyMetadata(false));

    public static object GetSuccessContent(DependencyObject obj)
    {
        return (object)obj.GetValue(SuccessContentProperty);
    }

    public static void SetSuccessContent(DependencyObject obj, object value)
    {
        obj.SetValue(SuccessContentProperty, value);
    }

    public static readonly DependencyProperty SuccessContentProperty =
        DependencyProperty.RegisterAttached("SuccessContent", typeof(object), typeof(Validation), new PropertyMetadata(null));

    #endregion

}
