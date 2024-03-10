namespace CelestialMapper.Core.Database;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public record StarDataRow
{
    [Key]
    [Column("id")]
    public required int Id { get; init; }

    [Column("hip")]
    public string Hip { get; init; } = string.Empty;

    [Column("hd")]
    public string Hd { get; init; } = string.Empty;

    [Column("hr")]
    public string Hr { get; init; } = string.Empty;

    [Column("gl")]
    public string Gl { get; init; } = string.Empty;

    [Column("bf")]
    public string Bf { get; init; } = string.Empty;

    [Column("proper")]
    public string Proper { get; init; } = string.Empty;

    [Column("ra")]
    public double Ra { get; init; }

    [Column("dec")]
    public double Dec { get; init; }

    [Column("dist")]
    public double Dist { get; init; }

    [Column("pmra")]
    public double PmRa { get; init; }

    [Column("pmdec")]
    public double PmDec { get; init; }

    [Column("rv")]
    public double Rv { get; init; }

    [Column("mag")]
    public double Mag { get; init; }

    [Column("absmag")]
    public double AbsMag { get; init; }

    [Column("spect")]
    public string Spect { get; init; } = string.Empty;

    [Column("ci")]
    public double Ci { get; init; }

    [Column("x")]
    public double X { get; init; }

    [Column("y")]
    public double Y { get; init; }

    [Column("z")]
    public double Z { get; init; }

    [Column("vx")]
    public double Vx { get; init; }

    [Column("vy")]
    public double Vy { get; init; }

    [Column("vz")]
    public double Vz { get; init; }

    [Column("rarad")]
    public int RaRad { get; init; }

    [Column("decrad")]
    public int DecRad { get; init; }

    [Column("pmrarad")]
    public int PmRaRad { get; init; }

    [Column("pmdecrad")]
    public int PmDecRad { get; init; }

    [Column("bayer")]
    public string Bayer { get; init; } = string.Empty;

    [Column("flam")]
    public string Flam { get; init; } = string.Empty;

    [Column("con")]
    public string Con { get; init; } = string.Empty;

    [Column("comp")]
    public int Comp { get; init; }

    [Column("comp_primary")]
    public int CompPrimary { get; init; }

    [Column("base")]
    public string Base { get; init; } = string.Empty;

    [Column("lum")]
    public int Lum { get; init; }

    [Column("var")]
    public string Var { get; init; } = string.Empty;

    [Column("var_min")]
    public string VarMin { get; init; } = string.Empty;

    [Column("var_max")]
    public string VarMax { get; init; } = string.Empty;
}