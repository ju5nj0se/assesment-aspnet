using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JuanJoseHernandez.Data.Repositories.Implementations;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly AppDbContext _context;

    public DepartmentsRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Department>> GetAllDepartments()
    {
        var department = await _context.Departments.ToListAsync();

        return department;
    }
}