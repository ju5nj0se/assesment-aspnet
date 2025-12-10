using JuanJoseHernandez.Data.Entities;

namespace JuanJoseHernandez.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<Department>> GetDepartmentsAsync();
        Task<IEnumerable<Degree>> GetDegreesAsync();
        Task<IEnumerable<EducationLevel>> GetEducationLevelsAsync();
        Task<IEnumerable<StatusEmployee>> GetStatusesAsync();

        Task<Department?> GetDepartmentByNameAsync(string name);
        Task<Degree?> GetDegreeByNameAsync(string name);
        Task<EducationLevel?> GetEducationLevelByNameAsync(string name);
        Task<StatusEmployee?> GetStatusByNameAsync(string name);
    }
}
