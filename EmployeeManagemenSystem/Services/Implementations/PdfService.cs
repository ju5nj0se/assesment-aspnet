using iTextSharp.text;
using iTextSharp.text.pdf;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Services.Interfaces;
using System.IO;

namespace JuanJoseHernandez.Services.Implementations
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateResumePdf(User user)
        {
            using (var ms = new MemoryStream())
            {
                var doc = new Document(PageSize.A4, 50, 50, 25, 25);
                var writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // Fonts
                var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18);
                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                var normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, new BaseColor(64, 64, 64));

                // Title
                var title = new Paragraph($"{user.Names} {user.LastNames}", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 5;
                doc.Add(title);

                var subtitle = new Paragraph(user.Degree?.Name ?? "Cargo no definido", headerFont);
                subtitle.Alignment = Element.ALIGN_CENTER;
                subtitle.SpacingAfter = 20;
                doc.Add(subtitle);

                // Divider line
                var line = new iTextSharp.text.pdf.draw.LineSeparator(1f, 100f, new BaseColor(192, 192, 192), Element.ALIGN_CENTER, -1);
                doc.Add(new Chunk(line));
                doc.Add(new Paragraph(" "));

                // Contact Info
                AddSectionTitle(doc, "Datos de Contacto");
                doc.Add(new Paragraph($"Email: {user.Email}", normalFont));
                doc.Add(new Paragraph($"Teléfono: {user.Telephone}", normalFont));
                doc.Add(new Paragraph($"Dirección: {user.Direction}", normalFont));
                doc.Add(new Paragraph(" "));

                // Labor Info
                AddSectionTitle(doc, "Información Laboral");
                var table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 30f, 70f });
                table.SpacingBefore = 10f;
                table.SpacingAfter = 10f;

                AddCell(table, "Departamento", labelFont);
                AddCell(table, user.Department?.Name ?? "-", normalFont);

                AddCell(table, "Cargo", labelFont);
                AddCell(table, user.Degree?.Name ?? "-", normalFont);

                AddCell(table, "Nivel Educativo", labelFont);
                AddCell(table, user.EducationLevel?.Name ?? "-", normalFont);

                AddCell(table, "Fecha Ingreso", labelFont);
                AddCell(table, user.DateEntry.ToString("dd/MM/yyyy"), normalFont);

                AddCell(table, "Salario", labelFont);
                AddCell(table, $"${user.Salary:N2}", normalFont);

                AddCell(table, "Estado", labelFont);
                AddCell(table, user.Status?.Status ?? "-", normalFont);

                doc.Add(table);

                // Profile
                if (!string.IsNullOrEmpty(user.Profile))
                {
                    AddSectionTitle(doc, "Perfil Profesional");
                    doc.Add(new Paragraph(user.Profile, normalFont));
                }

                // Footer
                doc.Close();
                return ms.ToArray();
            }
        }

        private void AddSectionTitle(Document doc, string titleText)
        {
            var font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, new BaseColor(0, 0, 255));
            var p = new Paragraph(titleText, font);
            p.SpacingBefore = 10f;
            p.SpacingAfter = 5f;
            doc.Add(p);
        }

        private void AddCell(PdfPTable table, string text, Font font)
        {
            var cell = new PdfPCell(new Phrase(text, font));
            cell.Padding = 5;
            cell.BorderColor = new BaseColor(192, 192, 192);
            table.AddCell(cell);
        }
    }
}
