using System.Windows.Media;

namespace CelestialMapper.UI;

public static class PositionHelper
{

    public static PositionInfo GetPositionInfo(this UIElement element, Visual relativeTo)
    {
        Point relativePoint = element.TransformToAncestor(relativeTo)
                              .Transform(new Point(0, 0));

        var (width, height) = (element.RenderSize.Width, element.RenderSize.Height);

        var topRight = new Point(relativePoint.X + width, relativePoint.Y);
        var bottomRight = new Point(relativePoint.X + width, relativePoint.Y + height);
        var bottomLeft = new Point(relativePoint.X, relativePoint.Y + height);
        return new(relativePoint, topRight, bottomRight, bottomLeft);
    }

}
