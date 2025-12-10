using JuanJoseHernandez.Data.Entities;

namespace JuanJoseHernandez.Data.Repositories.Interfaces
{
    public interface ICatalogRepository
    {
        Task<Department?> GetDepartmentByNameAsync(string name);
        Task<Degree?> GetDegreeByNameAsync(string name);
        Task<EducationLevel?> GetEducationLevelByNameAsync(string name);
        Task<StatusEmployee?> GetStatusByNameAsync(string name);

        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<IEnumerable<Degree>> GetDegreesAsync();
        Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync();
        Task<IEnumerable<StatusEmployee>> GetStatusesAsync();
    }
}
