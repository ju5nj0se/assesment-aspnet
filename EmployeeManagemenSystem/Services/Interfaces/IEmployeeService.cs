using JuanJoseHernandez.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace JuanJoseHernandez.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<User>> GetAllEmployeesAsync();
        Task<User?> GetEmployeeByIdAsync(string id);
        Task<IdentityResult> CreateEmployeeAsync(User employee, string? password);
        Task<IdentityResult> UpdateEmployeeAsync(User employee);
        Task<IdentityResult> DeleteEmployeeAsync(string id);
        Task<byte[]?> GenerateResumePdfAsync(string id);
        Task<bool> DocumentExistsAsync(string document, string? excludeUserId = null);
    }
}
