using JuanJoseHernandez.Data.Entities;

namespace JuanJoseHernandez.Services.Interfaces
{
    public interface IPdfService
    {
        byte[] GenerateResumePdf(User user);
    }
}
