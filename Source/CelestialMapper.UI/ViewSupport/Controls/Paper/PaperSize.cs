using System.Globalization;

namespace CelestialMapper.UI;

public enum PaperSize
{
    A3,
    A4,
    A5,
}

public static class PaperSizeExtensions
{

    private static readonly LengthConverter converter = new LengthConverter();

    public static (double Width, double Height) GetPaperSizeInPixels(this PaperSize paperSize)
    {
        var paperSizeCm = paperSize.GetPaperSizeInCentimeters();

        var widthInvariant = paperSizeCm.Width.ToString(CultureInfo.InvariantCulture);
        var heightInvariant = paperSizeCm.Height.ToString(CultureInfo.InvariantCulture);

        var width = (double)converter.ConvertFrom($"{widthInvariant}cm")!;
        var height = (double)converter.ConvertFrom($"{heightInvariant}cm")!;

        return (width, height);
    }

    public static (double Width, double Height) GetPaperSizeInCentimeters(this PaperSize paperSize)
    {
        return paperSize switch
        {
            PaperSize.A3 => (29.7, 42),
            PaperSize.A4 => (21, 29.7),
            PaperSize.A5 => (14.8, 21),
            _ => throw new NotImplementedException(),
        };
    }

}
