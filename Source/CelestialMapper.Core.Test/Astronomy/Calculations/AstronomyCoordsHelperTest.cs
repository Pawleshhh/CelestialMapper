using CelestialMapper.Common;
using PracticalAstronomy.CSharp;

namespace CelestialMapper.Core.Test;

[TestFixture]
public class AstronomyCoordsHelperTest
{
    [Test]
    public void MapCartesianCoords_Returns_Correct_Values()
    {
        Horizon horizon = new Horizon(45, 45);
        double mapDiameter = 100;

        double expectedX = 25 * MathHelper.SinD(45);
        double expectedY = 25 * MathHelper.CosD(45);

        var result = AstronomyCoordsHelper.MapCartesianCoords(horizon, mapDiameter);

        Assert.That(result.X, Is.EqualTo(expectedX).Within(0.000000001));
        Assert.That(result.Y, Is.EqualTo(expectedY).Within(0.000000001));
    }
}
