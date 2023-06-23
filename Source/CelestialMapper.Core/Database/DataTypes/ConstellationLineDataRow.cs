using System.ComponentModel.DataAnnotations;

namespace CelestialMapper.Core.Database;

public record ConstellationLineDataRow
{
    [Key]
    public required int Id { get; init; }

    [Required]
    public string Con { get; init; } = string.Empty;

    [Required]
    public int LineId { get; init; }

    [Required]
    public string Hr { get; init; } = string.Empty;
}