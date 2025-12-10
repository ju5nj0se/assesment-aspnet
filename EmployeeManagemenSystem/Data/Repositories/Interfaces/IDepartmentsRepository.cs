using JuanJoseHernandez.Data.Entities;

namespace JuanJoseHernandez.Data.Repositories.Interfaces;

public interface IDepartmentsRepository
{
    Task<List<Department>> GetAllDepartments();
}