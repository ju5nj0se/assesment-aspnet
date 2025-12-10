using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.DTOs;
using JuanJoseHernandez.Services.Interfaces;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JuanJoseHernandez.Controllers.Api
{
    [Route("api/auth")]
    [ApiController]
    public class ApiAuthController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICatalogService _catalogService;
        private readonly ITokenService _tokenService;
        private readonly IUserRepository _userRepository; // Needed for verify login and basic checks

        public ApiAuthController(IEmployeeService employeeService, ICatalogService catalogService, ITokenService tokenService, IUserRepository userRepository)
        {
            _employeeService = employeeService;
            _catalogService = catalogService;
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        [HttpGet("departments")]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _catalogService.GetDepartmentsAsync();
            return Ok(departments.Select(d => new { d.Id, d.Name }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterEmployeeDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Resolve Foreign Keys by Name
            var department = await _catalogService.GetDepartmentByNameAsync(dto.Departamento);
            if (department == null) return BadRequest($"Departamento '{dto.Departamento}' no válido.");

            var degree = await _catalogService.GetDegreeByNameAsync(dto.Cargo);
            if (degree == null) return BadRequest($"Cargo '{dto.Cargo}' no válido.");

            var educationLevel = await _catalogService.GetEducationLevelByNameAsync(dto.NivelEducativo);
            if (educationLevel == null) return BadRequest($"Nivel Educativo '{dto.NivelEducativo}' no válido.");

            var status = await _catalogService.GetStatusByNameAsync(dto.Estado);
            if (status == null) return BadRequest($"Estado '{dto.Estado}' no válido.");

            var user = new User
            {
                Document = dto.Documento,
                Names = dto.Nombres,
                LastNames = dto.Apellidos,
                BirthDate = DateOnly.FromDateTime(dto.FechaNacimiento),
                Direction = dto.Direccion,
                Telephone = dto.Telefono,
                Email = dto.Email,
                UserName = dto.Email, // Sync
                Salary = dto.Salario,
                DateEntry = DateOnly.FromDateTime(dto.FechaIngreso),
                Profile = dto.PerfilProfesional,
                DepartmentId = department.Id,
                DegreeId = degree.Id,
                EducationLevelId = educationLevel.Id,
                StatusId = status.Id
            };

            var result = await _employeeService.CreateEmployeeAsync(user, null); // Password null as requested/implied or no password handling in DTO
            
            // Wait, if no password, how do they login? The Login requirement is "Email + Document".
            // So password is technically not used for login in this custom flow.
            
            if (result.Succeeded)
            {
                // Return created user or token? Usually just success or token.
                // Let's return success message.
                return Ok(new { Message = "Empleado registrado exitosamente." });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return BadRequest(ModelState);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginApiDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Custom Login Logic: Verify Document
            if (user.Document != dto.Documento)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            // Generate JWT
            var token = _tokenService.GenerateJwtToken(user);
            return Ok(new { Token = token });
        }
    }
}
