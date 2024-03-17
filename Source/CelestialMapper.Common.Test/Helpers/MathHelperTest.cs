namespace CelestialMapper.Common.Test;

[TestFixture]
public class MathHelperTest
{

    private const double tolerance = Math.E;

    [TestCase(0, 0)]
    [TestCase(30, 0.5)]
    [TestCase(45, 0.707106781)]
    [TestCase(60, 0.866025404)]
    [TestCase(90, 1)]
    [TestCase(180, 0)]
    [TestCase(270, -1)]
    [TestCase(360, 0)]
    public void SinD_Returns_Correct_Values(double degrees, double expected)
    {
        Assert.That(MathHelper.SinD(degrees), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(0, 1)]
    [TestCase(30, 0.866025404)]
    [TestCase(45, 0.707106781)]
    [TestCase(60, 0.5)]
    [TestCase(90, 0)]
    [TestCase(180, -1)]
    [TestCase(270, 0)]
    [TestCase(360, 1)]
    public void CosD_Returns_Correct_Values(double degrees, double expected)
    {
        Assert.That(MathHelper.CosD(degrees), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(0, 0)]
    [TestCase(30, 0.577350269)]
    [TestCase(45, 1)]
    [TestCase(60, 1.732050808)]
    [TestCase(89, 57.290000695)]
    public void TanD_Returns_Correct_Values(double degrees, double expected)
    {
        Assert.That(MathHelper.TanD(degrees), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(0, 0)]
    [TestCase(0.5, 30)]
    [TestCase(0.707106781, 45)]
    [TestCase(0.866025404, 60)]
    [TestCase(1, 90)]
    [TestCase(-1, -90)]
    public void AsinD_Returns_Correct_Values(double sinValue, double expected)
    {
        Assert.That(MathHelper.AsinD(sinValue), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(1, 0)]
    [TestCase(0.866025404, 30)]
    [TestCase(0.707106781, 45)]
    [TestCase(0.5, 60)]
    [TestCase(0, 90)]
    [TestCase(-1, 180)]
    [TestCase(0, 90)]
    [TestCase(1, 0)]
    public void AcosD_Returns_Correct_Values(double cosValue, double expected)
    {
        Assert.That(MathHelper.AcosD(cosValue), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(0, 0)]
    [TestCase(0.577350269, 30)]
    [TestCase(1, 45)]
    [TestCase(1.732050808, 60)]
    [TestCase(57.290000695, 89)]
    public void AtanD_Returns_Correct_Values(double tanValue, double expected)
    {
        Assert.That(MathHelper.AtanD(tanValue), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(0, 0)]
    [TestCase(30, 0.523598776)]
    [TestCase(45, 0.785398163)]
    [TestCase(60, 1.047197551)]
    [TestCase(90, 1.570796327)]
    [TestCase(180, 3.141592654)]
    [TestCase(270, 4.71238898)]
    [TestCase(360, 6.283185307)]
    public void DegreesToRadians_Returns_Correct_Values(double degrees, double expected)
    {
        Assert.That(MathHelper.DegreesToRadians(degrees), Is.EqualTo(expected).Within(tolerance));
    }

    [TestCase(0, 0)]
    [TestCase(0.523598776, 30)]
    [TestCase(0.785398163, 45)]
    [TestCase(1.047197551, 60)]
    [TestCase(1.570796327, 90)]
    [TestCase(3.141592654, 180)]
    [TestCase(4.71238898, 270)]
    [TestCase(6.283185307, 360)]
    public void RadiansToDegrees_Returns_Correct_Values(double radians, double expected)
    {
        Assert.That(MathHelper.RadiansToDegrees(radians), Is.EqualTo(expected).Within(tolerance));
    }
}
