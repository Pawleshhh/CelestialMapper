using System.IO;
using System.Windows.Media;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace CelestialMapper.UI;

public class VisualToPdf : IVisualToPdf
{
    public void Convert(Visual visual, string pdfPath)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(memoryStream, FileMode.Create);
            XpsDocument xpsDocument = new XpsDocument(package);
            XpsDocumentWriter xpsDocumentWriter = XpsDocument.CreateXpsDocumentWriter(xpsDocument);

            xpsDocumentWriter.Write(visual);
            xpsDocument.Close();
            package.Close();

            var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(memoryStream);
            PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, pdfPath, 0);
        }
    }
}
