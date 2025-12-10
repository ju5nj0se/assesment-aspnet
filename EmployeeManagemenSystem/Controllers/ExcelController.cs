using System.Text.RegularExpressions;
using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Data;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.DTOs;
using JuanJoseHernandez.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace JuanJoseHernandez.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class ExcelController : Controller
    {
        private readonly IUserImportService _importService;

        public ExcelController(IUserImportService importService)
        {
            _importService = importService;
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Por favor seleccione un archivo.");
                return View("Upload");
            }

            if (!Path.GetExtension(file.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Solo se permiten archivos .xlsx");
                return View("Upload");
            }

            var allErrors = new List<string>();
            var totalSuccess = 0;
            var totalUpdated = 0;

            try
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        if (worksheet.Dimension == null)
                        {
                            ModelState.AddModelError("", "El archivo Excel está vacío.");
                            return View("Upload");
                        }

                        var rowCount = worksheet.Dimension.Rows;
                        var batch = new List<ExcelDataDto>();
                        
                        // Loop from row 2 to skip header
                        for (int row = 2; row <= rowCount; row++)
                        {
                            try 
                            {
                                var dto = new ExcelDataDto
                                {
                                    Documento = GetString(worksheet, row, 1),
                                    Nombres = GetString(worksheet, row, 2),
                                    Apellidos = GetString(worksheet, row, 3),
                                    FechaNacimiento = GetDate(worksheet, row, 4), // Mapped to col 4
                                    Direccion = GetString(worksheet, row, 5),
                                    Telefono = GetString(worksheet, row, 6),
                                    Email = GetString(worksheet, row, 7),
                                    Cargo = GetString(worksheet, row, 8),
                                    Salario = GetDecimal(worksheet, row, 9),
                                    FechaIngreso = GetDate(worksheet, row, 10),
                                    Estado = GetString(worksheet, row, 11),
                                    NivelEducativo = GetString(worksheet, row, 12),
                                    PerfilProfesional = GetString(worksheet, row, 13),
                                    Departamento = GetString(worksheet, row, 14)
                                };

                                batch.Add(dto);
                            }
                            catch (Exception ex)
                            {
                                allErrors.Add($"Fila {row}: Error leyendo datos - {ex.Message}");
                            }

                            // Process batch when size 10 or last row
                            if (batch.Count == 10 || row == rowCount)
                            {
                                if (batch.Any())
                                {
                                    var result = await _importService.ProcessBatchAsync(batch);
                                    allErrors.AddRange(result.Errors);
                                    totalSuccess += result.SuccessCount;
                                    totalUpdated += result.UpdatedCount;
                                    batch.Clear();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                allErrors.Add($"Error crítico procesando el archivo: {ex.Message}");
            }

            ViewBag.Errors = allErrors;
            ViewBag.SuccessMessage = $"Proceso finalizado. Creados: {totalSuccess}, Actualizados: {totalUpdated}.";
            return View("Upload");
        }

        // Helpers for safe Excel reading
        private string GetString(ExcelWorksheet ws, int row, int col) => ws.Cells[row, col].Value?.ToString()?.Trim() ?? "";

        private decimal GetDecimal(ExcelWorksheet ws, int row, int col) 
        {
            var val = ws.Cells[row, col].Value;
            if (val == null) return 0;
            if (val is double d) return (decimal)d;
            if (val is decimal dec) return dec;
            if (decimal.TryParse(val.ToString(), out decimal res)) return res;
            throw new Exception($"Valor numérico inválido en columna {col}");
        }

        private DateTime GetDate(ExcelWorksheet ws, int row, int col)
        {
            var val = ws.Cells[row, col].Value;
            if (val == null) return DateTime.MinValue;
            if (val is DateTime dt) return dt;
            if (DateTime.TryParse(val.ToString(), out DateTime res)) return res;
            throw new Exception($"Fecha inválida en columna {col}");
        }
    }
}