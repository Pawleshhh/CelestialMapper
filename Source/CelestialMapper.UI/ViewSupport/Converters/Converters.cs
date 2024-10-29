namespace CelestialMapper.UI;

public class Converters
{

    #region Visibility

    public static BooleanToVisibilityConverter FalseMakesMeCollapsed { get; } = new();

    public static NullToVisibilityConverter NullMakesMeCollapsed { get; } = new();

    #endregion

    #region Math

    public static NegateValueConverter NegateMe { get; } = new();

    public static DivideByConverter DivideBy { get; } = new();

    public static DivideByConverter HalfValue { get; } = new() { Parameter = 2 };

    public static ConverterGroup<double, double, double> NegateHalfValue { get; } = new(NegateMe, HalfValue);

    #endregion
}
