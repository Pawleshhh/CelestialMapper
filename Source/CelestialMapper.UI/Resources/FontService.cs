using System.Windows.Media;

namespace CelestialMapper.UI;

[Export(typeof(IFontService), typeof(FontService), IsKeyed = false, IsSingleton = true, Key = nameof(FontService))]
public class FontService : IFontService
{

    private string[]? cachedFonts;

    public string[] GetFonts()
    {
        if (this.cachedFonts is null)
        {
            this.cachedFonts = Fonts.SystemFontFamilies.Select(f => f.Source).ToArray();
        }

        return this.cachedFonts;
    }

    public double GetFontSize()
    {
        return 16;
    }
}
