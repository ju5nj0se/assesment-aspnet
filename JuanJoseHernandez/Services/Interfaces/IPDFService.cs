namespace JuanJoseHernandez.Services.Interfaces;

public interface IPDFService
{
    Task<byte[]> CreatePdf();
    Task<byte[]> FullPdf();
}