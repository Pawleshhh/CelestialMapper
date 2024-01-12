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
    [Column("line_id")]
    public int LineId { get; init; }

    [Required]
    [Column("hr")]
    public string Hr { get; init; } = string.Empty;
}