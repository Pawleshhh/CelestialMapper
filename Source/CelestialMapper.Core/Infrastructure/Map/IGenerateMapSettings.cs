using CelestialMapper.Common;

namespace CelestialMapper.Core.Infrastructure.Map;

public interface IGenerateMapSettings
{

    public NumRange<double> MagnitudeRange { get; }

    public static IGenerateMapSettings Create(NumRange<double> magnitudeRange)
        => new GenerateMapSettingsBase
        {
            MagnitudeRange = magnitudeRange
        };

}

internal record GenerateMapSettingsBase : IGenerateMapSettings
{
    public required NumRange<double> MagnitudeRange { get; init; }
}