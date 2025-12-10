using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JuanJoseHernandez.Controllers.Api
{
    [Route("api")]
    [ApiController]
    public class ApiEmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public ApiEmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize(AuthenticationSchemes = "Bearer")] 
        [HttpGet("me")]
        public async Task<IActionResult> GetMe()
        {
            // Try to extract ID from multiple common claim types
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                         ?? User.FindFirst(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized("No se pudo identificar el usuario desde el token.");

            var user = await _employeeService.GetEmployeeByIdAsync(userId);
            if (user == null) return NotFound("Usuario no encontrado.");

            // Return Full Profile
            return Ok(new 
            {
                user.Id,
                user.Document,
                user.Names,
                user.LastNames,
                user.Email,
                user.BirthDate,
                user.Direction,
                user.Telephone,
                user.Salary,
                user.Profile,
                user.DateEntry,
                Departamento = user.Department?.Name,
                Cargo = user.Degree?.Name,
                NivelEducativo = user.EducationLevel?.Name,
                Status = user.Status?.Status
            });
        }

        [Authorize(AuthenticationSchemes = "Bearer")] 
        [HttpGet("me/resume")]
        public async Task<IActionResult> DownloadResume()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                         ?? User.FindFirst(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub)?.Value
                         ?? User.FindFirst("sub")?.Value;

            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var pdfBytes = await _employeeService.GenerateResumePdfAsync(userId);
            if (pdfBytes == null) return NotFound("No se pudo generar la hoja de vida.");

            var user = await _employeeService.GetEmployeeByIdAsync(userId);
            string filename = (user != null) ? $"HojaVida_{user.Names}_{user.LastNames}.pdf" : "HojaVida.pdf";

            return File(pdfBytes, "application/pdf", filename);
        }
    }
}
