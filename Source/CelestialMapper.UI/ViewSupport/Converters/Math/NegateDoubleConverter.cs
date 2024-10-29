namespace CelestialMapper.UI;

public class NegateValueConverter : MathConverter<double>
{
    protected override Func<double?, double?, double?> ConvertValue => (v, p) => -v;
    protected override Func<double?, double?, double?> ConvertValueBack => (v, p) => -v;
}
