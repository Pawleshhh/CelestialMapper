namespace CelestialMapper.Common.Test;

[TestFixture]
public class NumRangeTest
{

    [TestCase(0d, true)]
    [TestCase(5d, true)]
    [TestCase(4.99, true)]
    [TestCase(0.01, true)]
    [TestCase(-0.001, false)]
    [TestCase(5.001, false)]
    [TestCase(-0.5, false)]
    [TestCase(6, false)]
    public void InRange_WithGivenValueAndBothInclusive_ReturnsTrueIfInRange(double value, bool expected)
    {
        var range = NumRange.Of(0d, 5d);

        var result = range.InRange(value);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(0d, false)]
    [TestCase(5d, false)]
    [TestCase(4.99, true)]
    [TestCase(0.01, true)]
    [TestCase(-0.001, false)]
    [TestCase(5.001, false)]
    [TestCase(-0.5, false)]
    [TestCase(6, false)]
    public void InRange_WithGivenValueAndBothExclusive_ReturnsTrueIfInRange(double value, bool expected)
    {
        var range = NumRange.Of(0d, 5d);

        var result = range.InRange(value, NumRangeKind.Exclusive);

        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(2d,    NumRangeKind.Inclusive, NumRangeKind.Exclusive, true)]
    [TestCase(5d,    NumRangeKind.Exclusive, NumRangeKind.Inclusive, true)]
    [TestCase(1d,    NumRangeKind.Exclusive, NumRangeKind.Exclusive, false)]
    [TestCase(5d,    NumRangeKind.Inclusive, NumRangeKind.Inclusive, true)]
    [TestCase(1d,    NumRangeKind.Inclusive, NumRangeKind.Inclusive, true)]
    [TestCase(1.001, NumRangeKind.Inclusive, NumRangeKind.Exclusive, true)]
    [TestCase(0.999, NumRangeKind.Exclusive, NumRangeKind.Inclusive, false)]
    [TestCase(0.999, NumRangeKind.Inclusive, NumRangeKind.Inclusive, false)]
    [TestCase(5.001, NumRangeKind.Inclusive, NumRangeKind.Inclusive, false)]
    [TestCase(1.001, NumRangeKind.Exclusive, NumRangeKind.Exclusive, true)]
    public void InRange_WithGivenValueAndGivenNumRangeKind_ReturnsTrueIfInRange(double value, NumRangeKind minKind, NumRangeKind maxKind, bool expected)
    {
        var range = NumRange.Of(1d, 5d);

        var result = range.InRange(value, minKind, maxKind);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void MinValue_ReturnsCorrectValue()
    {
        var range = new NumRange<int>();

        Assert.That(range.Min, Is.EqualTo(int.MinValue));
    }

    [Test]
    public void MaxValue_ReturnsCorrectValue()
    {
        var range = new NumRange<int>();

        Assert.That(range.Max, Is.EqualTo(int.MaxValue));
    }
}
