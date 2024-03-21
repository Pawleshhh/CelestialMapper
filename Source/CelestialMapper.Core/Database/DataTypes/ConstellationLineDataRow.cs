using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CelestialMapper.Core.Database;

public record ConstellationLineDataRow
{
    [Key]
    [Column("id")]
    public required int Id { get; init; }

    [Required]
    [Column("con")]
    public string Con { get; init; } = string.Empty;

    [Required]
    [Column("lineid")]
    public int LineId { get; init; }

    [Required]
    [Column("hr")]
    public string Hr { get; init; } = string.Empty;
}

public record ConstellationLineDataRowPosition : ConstellationLineDataRow
{
    [Column("ra")]
    public double Ra { get; init; }

    [Column("dec")]
    public double Dec { get; init; }
}