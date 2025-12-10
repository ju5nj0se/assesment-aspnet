using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using JuanJoseHernandez.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace JuanJoseHernandez.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPdfService _pdfService;

        public EmployeeService(IUserRepository userRepository, IPdfService pdfService)
        {
            _userRepository = userRepository;
            _pdfService = pdfService;
        }

        public async Task<IEnumerable<User>> GetAllEmployeesAsync()
        {
            return await _userRepository.GetAllWithDetailsAsync();
        }

        public async Task<User?> GetEmployeeByIdAsync(string id)
        {
            return await _userRepository.GetByIdWithDetailsAsync(id); // Para edición y PDF necesitamos detalles
        }

        public async Task<IdentityResult> CreateEmployeeAsync(User employee, string? password)
        {
            // Business Logic: Check unique document
            if (await DocumentExistsAsync(employee.Document))
            {
                 return IdentityResult.Failed(new IdentityError { Description = "El documento ya está registrado por otro empleado." });
            }

            // Set default values
            employee.UserName = employee.Email; // Keep synced
            employee.SecurityStamp = Guid.NewGuid().ToString();

            IdentityResult result;
            if (string.IsNullOrEmpty(password))
            {
                result = await _userRepository.CreateAsync(employee);
            }
            else
            {
                result = await _userRepository.CreateAsync(employee, password);
            }

            if (result.Succeeded)
            {
                await _userRepository.AddToRoleAsync(employee, Roles.User);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateEmployeeAsync(User employee)
        {
             // Business Logic: Check unique document excluding current user
            if (await DocumentExistsAsync(employee.Document, employee.Id))
            {
                 return IdentityResult.Failed(new IdentityError { Description = "El documento ya está registrado por otro empleado." });
            }

            // Fetch original to keep critical fields safed if needed, or assume controller passed correct full object
            // Ideally we should fetch here and update fields, but for now we trust the passed object or fetch it
            var existingUser = await _userRepository.GetByIdAsync(employee.Id);
            if (existingUser == null) return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado." });

            // Update fields
            existingUser.Document = employee.Document;
            existingUser.Names = employee.Names;
            existingUser.LastNames = employee.LastNames;
            existingUser.BirthDate = employee.BirthDate;
            existingUser.Email = employee.Email;
            existingUser.UserName = employee.Email;
            existingUser.Telephone = employee.Telephone;
            existingUser.Direction = employee.Direction;
            existingUser.Salary = employee.Salary;
            existingUser.DateEntry = employee.DateEntry;
            existingUser.Profile = employee.Profile;
            existingUser.DepartmentId = employee.DepartmentId;
            existingUser.DegreeId = employee.DegreeId;
            existingUser.EducationLevelId = employee.EducationLevelId;
            existingUser.StatusId = employee.StatusId;

            return await _userRepository.UpdateAsync(existingUser);
        }

        public async Task<IdentityResult> DeleteEmployeeAsync(string id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return IdentityResult.Failed(new IdentityError { Description = "Usuario no encontrado." });
            
            return await _userRepository.DeleteAsync(user);
        }

        public async Task<byte[]?> GenerateResumePdfAsync(string id)
        {
            var user = await _userRepository.GetByIdWithDetailsAsync(id);
            if (user == null) return null;

            return _pdfService.GenerateResumePdf(user);
        }

        public async Task<bool> DocumentExistsAsync(string document, string? excludeUserId = null)
        {
            var user = await _userRepository.GetByDocumentAsync(document);
            if (user == null) return false;
            
            if (excludeUserId != null && user.Id == excludeUserId) return false;

            return true;
        }
    }
}
