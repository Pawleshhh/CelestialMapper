using System.Windows.Media;

namespace CelestialMapper.UI;

public static class RenderTransformHelper
{

    public static TransformGroup CreateTransformGroup(params Transform[] transforms)
    {
        var transformGroup = new TransformGroup();
        foreach (var transform in transforms)
        {
            transformGroup.Children.Add(transform);
        }

        return transformGroup;
    }

}
