namespace CelestialMapper.Core.Astronomy;

public record Constellation(
    long Id,
    string Name,
    string ShortName,
    IEnumerable<ConstellationLine> ConstellationLines);
