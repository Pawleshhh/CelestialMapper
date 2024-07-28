using FunctionalCSharp;

namespace CelestialMapper.UI;

public record PositionInfo(Point TopLeft, Point TopRight, Point BottomRight, Point BottomLeft)
{

    public double Width => (TopRight.X - TopLeft.X).Into(Math.Abs);

    public double Height => (BottomLeft.Y - TopLeft.Y).Into(Math.Abs);

}
