using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JuanJoseHernandez.Data.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User?> GetByIdWithDetailsAsync(string id)
        {
             return await _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.Degree)
                .Include(u => u.EducationLevel)
                .Include(u => u.Status)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        
        public async Task<User?> GetByDocumentAsync(string document)
        {
            return await _userManager.Users.FirstOrDefaultAsync(u => u.Document == document);
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await _userManager.Users
                .Include(u => u.Department)
                .Include(u => u.Degree)
                .Include(u => u.Status)
                .OrderBy(u => u.Names)
                .ToListAsync();
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(User user)
        {
            return await _userManager.CreateAsync(user);
        }

        public async Task<IdentityResult> UpdateAsync(User user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteAsync(User user)
        {
            return await _userManager.DeleteAsync(user);
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
