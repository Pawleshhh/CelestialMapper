using System.Windows.Media;

namespace CelestialMapper.UI;

public static class Rotate
{
    public static RotateTransform By(double angle)
        => new RotateTransform(angle);
}

public static class Scale
{
    public static ScaleTransform By(double x, double y)
        => new ScaleTransform(x, y);

    public static ScaleTransform ByX(double x)
        => new(x, 1);

    public static ScaleTransform ByY(double y)
        => new(1, y);
}

public static class Translate
{
    public static TranslateTransform To(double x, double y)
        => new TranslateTransform(x, y);
}