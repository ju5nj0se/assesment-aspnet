using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JuanJoseHernandez.Data.Repositories.Implementations
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly AppDbContext _context;

        public CatalogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Department?> GetDepartmentByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            return await _context.Departments.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<Degree?> GetDegreeByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            return await _context.Degrees.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<EducationLevel?> GetEducationLevelByNameAsync(string name)
        {
             if (string.IsNullOrWhiteSpace(name)) return null;
            return await _context.EducationLevels.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }

        public async Task<StatusEmployee?> GetStatusByNameAsync(string name)
        {
             if (string.IsNullOrWhiteSpace(name)) return null;
            return await _context.StatusEmployees.FirstOrDefaultAsync(x => x.Status.ToLower() == name.ToLower());
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<IEnumerable<Degree>> GetDegreesAsync()
        {
            return await _context.Degrees.ToListAsync();
        }

        public async Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync()
        {
            return await _context.EducationLevels.ToListAsync();
        }

        public async Task<IEnumerable<StatusEmployee>> GetStatusesAsync()
        {
            return await _context.StatusEmployees.ToListAsync();
        }
    }
}
