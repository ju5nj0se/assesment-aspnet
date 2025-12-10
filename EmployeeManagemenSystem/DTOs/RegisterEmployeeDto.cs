using System.ComponentModel.DataAnnotations;

namespace JuanJoseHernandez.DTOs
{
    public class RegisterEmployeeDto
    {
        [Required]
        public string Documento { get; set; } = string.Empty;

        [Required]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Cargo { get; set; } = string.Empty;

        [Required]
        public decimal Salario { get; set; }

        [Required]
        public DateTime FechaIngreso { get; set; }

        [Required]
        public string Estado { get; set; } = string.Empty;

        [Required]
        public string NivelEducativo { get; set; } = string.Empty;

        [Required]
        public string PerfilProfesional { get; set; } = string.Empty;

        [Required]
        public string Departamento { get; set; } = string.Empty;
    }
}
