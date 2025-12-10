using System.ComponentModel.DataAnnotations;
using JuanJoseHernandez.Constants;

namespace JuanJoseHernandez.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = ValidationMessages.EmailRequired)]
        [EmailAddress(ErrorMessage = ValidationMessages.InvalidEmailFormat)]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}
