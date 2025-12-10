using JuanJoseHernandez.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace JuanJoseHernandez.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(string id);
        Task<User?> GetByIdWithDetailsAsync(string id);
        Task<User?> GetByDocumentAsync(string document);
        Task<IEnumerable<User>> GetAllWithDetailsAsync();
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> CreateAsync(User user);
        Task<IdentityResult> UpdateAsync(User user);
        Task<IdentityResult> DeleteAsync(User user);
        Task AddToRoleAsync(User user, string role);
    }
}
