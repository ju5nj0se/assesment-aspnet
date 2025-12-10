using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using JuanJoseHernandez.Services.Interfaces;

namespace JuanJoseHernandez.Services.Implementations
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogService(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            return await _catalogRepository.GetDepartmentsAsync();
        }

        public async Task<IEnumerable<Degree>> GetDegreesAsync()
        {
            return await _catalogRepository.GetDegreesAsync();
        }

        public async Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync()
        {
            return await _catalogRepository.GetEducationLevelsAsync();
        }

        public async Task<IEnumerable<StatusEmployee>> GetStatusesAsync()
        {
            return await _catalogRepository.GetStatusesAsync();
        }

        public async Task<Department?> GetDepartmentByNameAsync(string name) => await _catalogRepository.GetDepartmentByNameAsync(name);
        public async Task<Degree?> GetDegreeByNameAsync(string name) => await _catalogRepository.GetDegreeByNameAsync(name);
        public async Task<EducationLevel?> GetEducationLevelByNameAsync(string name) => await _catalogRepository.GetEducationLevelByNameAsync(name);
        public async Task<StatusEmployee?> GetStatusByNameAsync(string name) => await _catalogRepository.GetStatusByNameAsync(name);
    }
}
