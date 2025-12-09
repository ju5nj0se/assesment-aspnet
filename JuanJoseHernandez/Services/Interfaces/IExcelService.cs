using JuanJoseHernandez.DTOs;

namespace JuanJoseHernandez.Services.Interfaces;

public interface IExcelService
{
    Task<byte[]> CrearReporteVentas(List<VentaDto> ventas);
}