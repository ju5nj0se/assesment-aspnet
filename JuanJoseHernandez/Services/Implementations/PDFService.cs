using iTextSharp.text;
using iTextSharp.text.pdf;
using JuanJoseHernandez.Services.Interfaces;

namespace JuanJoseHernandez.Services.Implementations;

public class PDFService : IPDFService
{
    public async Task<byte[]> CreatePdf()
    {
        // 1. Ruta destino
        using var ms = new MemoryStream();
        using var doc = new Document(PageSize.A4);
        PdfWriter.GetInstance(doc, ms);

        doc.Open();

        // =========================
        // TÍTULO
        // =========================
        //1er param es el contenido y el segundo es el formato o estilo
        Paragraph titulo = new Paragraph("Titulo", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
        titulo.Alignment = Element.ALIGN_CENTER;
        titulo.SpacingAfter = 70;
        doc.Add(titulo);

        // =========================
        // TEXTO NORMAL Y NEGRITAS
        // =========================

        Paragraph p = new Paragraph();
        p.Add(new Phrase("Este es un texto normal. ", FontFactory.GetFont(FontFactory.HELVETICA, 12)));
        p.Add(new Phrase("Esto está en negritas.", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12)));
        p.SpacingAfter =70;

        doc.Add(p);

        doc.Close();

        return ms.ToArray();
    }
    
    public async Task<byte[]> FullPdf()
    {
        Document doc = new Document(PageSize.A4);
        var ms  = new MemoryStream();
        PdfWriter.GetInstance(doc, ms);
        doc.Open();
    
        var tituloFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20);
        var bold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
        var normal = FontFactory.GetFont(FontFactory.HELVETICA, 12);
    
        // Título
        Paragraph titulo = new Paragraph("Reporte General", tituloFont);
        titulo.Alignment = Element.ALIGN_CENTER;
        titulo.SpacingAfter = 20;
        doc.Add(titulo);
    
        // Imagen
        var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "log.jpeg");
        Image img = Image.GetInstance(imgPath);

        img.ScaleToFit(100, 100);
        img.Alignment = Image.ALIGN_CENTER;
        img.SpacingAfter = 20;
        doc.Add(img);
    
        // Texto
        Paragraph p = new Paragraph("Este es un reporte generado con iTextSharp.\n\n", normal);
        doc.Add(p);
    
        // Tabla
        PdfPTable tabla = new PdfPTable(3);
        tabla.WidthPercentage = 100;
    
        tabla.AddCell(new PdfPCell(new Phrase("ID", bold)));
        tabla.AddCell(new PdfPCell(new Phrase("Producto", bold)));
        tabla.AddCell(new PdfPCell(new Phrase("Precio", bold)));
    
        for (int i = 1; i <= 5; i++)
        {
            tabla.AddCell(i.ToString());
            tabla.AddCell("Producto " + i);
            tabla.AddCell("$" + (i * 15));
        }
    
        tabla.SpacingBefore = 20;
        doc.Add(tabla);
    
        doc.Close();
        
        return ms.ToArray();
    }
}