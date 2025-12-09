namespace JuanJoseHernandez.DTOs;

public class VentaDto
{
    public int Id { get; set; }
    public string Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal Total => Cantidad * PrecioUnitario;
}