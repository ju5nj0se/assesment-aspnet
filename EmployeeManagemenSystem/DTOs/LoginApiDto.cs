using System.ComponentModel.DataAnnotations;

namespace JuanJoseHernandez.DTOs
{
    public class LoginApiDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Documento { get; set; } = string.Empty;
    }
}
