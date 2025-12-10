using JuanJoseHernandez.Data.Entities;

namespace JuanJoseHernandez.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(User user);
    }
}
