namespace CelestialMapper.UI;

public class DivideByConverter : MathConverter<double>
{
    protected override Func<double?, double?, double?> ConvertValue => (v, p) => v / p;

    protected override Func<double?, double?, double?> ConvertValueBack => (v, p) => v * p;
}
