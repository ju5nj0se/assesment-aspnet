using System.Drawing;
using JuanJoseHernandez.DTOs;
using JuanJoseHernandez.Services.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace JuanJoseHernandez.Services.Implementations;

public class ExcelService : IExcelService
{
    public async Task<byte[]> CrearReporteVentas(List<VentaDto> ventas)
    {
        using var package = new ExcelPackage();
        var ws = package.Workbook.Worksheets.Add("Reporte");

        // === ENCABEZADO =================================================================
        ws.Cells["A1:D1"].Merge = true;
        ws.Cells["A1"].Value = "Reporte de Ventas";
        ws.Cells["A1"].Style.Font.Bold = true;
        ws.Cells["A1"].Style.Font.Size = 20;
        ws.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        ws.Row(1).Height = 35;

        // Logo (opcional)
        var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "Services", "Files", "logo.png");
        if (File.Exists(logoPath))
        {
            var img = ws.Drawings.AddPicture("Logo", new FileInfo(logoPath));
            img.SetPosition(0, 0);  // fila, columna
            img.SetSize(90);
        }

        // === CABECERAS DE LA TABLA =====================================================
        ws.Cells["A3"].Value = "ID";
        ws.Cells["B3"].Value = "Producto";
        ws.Cells["C3"].Value = "Cantidad";
        ws.Cells["D3"].Value = "Precio Unit.";
        ws.Cells["E3"].Value = "Total";

        using (var range = ws.Cells["A3:E3"])
        {
            range.Style.Font.Bold = true;
            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
            range.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(52, 73, 94));
            range.Style.Font.Color.SetColor(Color.White);
            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        // === CUERPO ====================================================================
        int row = 4;

        foreach (var v in ventas)
        {
            ws.Cells[row, 1].Value = v.Id;
            ws.Cells[row, 2].Value = v.Producto;
            ws.Cells[row, 3].Value = v.Cantidad;
            ws.Cells[row, 4].Value = v.PrecioUnitario;
            ws.Cells[row, 4].Style.Numberformat.Format = "#,##0.00";

            ws.Cells[row, 5].Formula = $"=C{row}*D{row}";
            ws.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";

            row++;
        }

        // === TOTAL GENERAL ==============================================================
        ws.Cells[row, 4].Value = "TOTAL";
        ws.Cells[row, 4].Style.Font.Bold = true;

        ws.Cells[row, 5].Formula = $"=SUM(E4:E{row - 1})";
        ws.Cells[row, 5].Style.Numberformat.Format = "#,##0.00";
        ws.Cells[row, 5].Style.Font.Bold = true;

        // === BORDES =====================================================================
        using (var range = ws.Cells[$"A3:E{row}"])
        {
            range.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        // === AJUSTES FINALES ============================================================
        ws.Column(2).Width = 25;   // Producto más ancho
        ws.Column(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        return package.GetAsByteArray();
    }
}