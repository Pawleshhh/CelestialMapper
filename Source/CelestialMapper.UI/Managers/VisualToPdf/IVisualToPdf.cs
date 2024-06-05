using System.Windows.Media;
using System.Windows.Xps.Packaging;

namespace CelestialMapper.UI;

public interface IVisualToPdf
{

    public void XpsToPdf(Visual visual, string pdfPath);

}
