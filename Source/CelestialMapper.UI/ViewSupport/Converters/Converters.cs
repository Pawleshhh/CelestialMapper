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

    #region Text

    public static BoolToFontStyleConverter BoolToFontStyle { get; } = new();

    public static BoolToFontWeightConverter BoolToFontWeight { get; } = new();

    public static BoolToTextWrapping BoolToTextWrapping { get; } = new();

    public static TextHorizontalAlignmentConverter TextHorizontalAlignment { get; } = new();

    public static TextDecorationsConverter TextDecorations { get; } = new();

    public static InvertBooleanToVisibilityConverter InvertBoolToVisibility { get; } = new();

    #endregion

    #region Property Editing

    public static EnumValuesConverter EnumValues { get; } = new();

    #endregion
}
