namespace CelestialMapper.Core.Database;

using System.ComponentModel.DataAnnotations;

public record StarDataRow
{
    [Key]
    public required int Id { get; init; }

    public string Hip { get; init; } = string.Empty;

    public string Hd { get; init; } = string.Empty;

    public string Hr { get; init; } = string.Empty;

    public string Gl { get; init; } = string.Empty;

    public string Bf { get; init; } = string.Empty;

    public string Proper { get; init; } = string.Empty;

    public double Ra { get; init; }

    public double Dec { get; init; }

    public double Dist { get; init; }

    public double PmRa { get; init; }

    public double PmDec { get; init; }

    public double Rv { get; init; }

    public double Mag { get; init; }

    public double AbsMag { get; init; }

    public string Spect { get; init; } = string.Empty;

    public double Ci { get; init; }

    public double X { get; init; }

    public double Y { get; init; }

    public double Z { get; init; }

    public double Vx { get; init; }

    public double Vy { get; init; }

    public double Vz { get; init; }

    public int RaRad { get; init; }

    public int DecRad { get; init; }

    public int PmRaRad { get; init; }

    public int PmDecRad { get; init; }

    public string Bayer { get; init; } = string.Empty;

    public string Flam { get; init; } = string.Empty;

    public string Con { get; init; } = string.Empty;

    public int Comp { get; init; }

    public int CompPrimary { get; init; }

    public string Base { get; init; } = string.Empty;

    public int Lum { get; init; }

    public string Var { get; init; } = string.Empty;

    public string VarMin { get; init; } = string.Empty;

    public string VarMax { get; init; } = string.Empty;
}