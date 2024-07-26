namespace CelestialMapper.UI;

public class Converters
{

    #region Visibility

    public static BooleanToVisibilityConverter FalseMakesMeCollapsed { get; } = new BooleanToVisibilityConverter();

    #endregion

    #region Math

    public static NegateValueConverter NegateMe { get; } = new NegateValueConverter();

    public static DivideByConverter DivideBy { get; } = new DivideByConverter();

    public static DivideByConverter HalfValue { get; } = new DivideByConverter { Parameter = 2 };

    public static ConverterGroup<double, double, double> NegateHalfValue { get; } = new(NegateMe, HalfValue);

    #endregion
}
