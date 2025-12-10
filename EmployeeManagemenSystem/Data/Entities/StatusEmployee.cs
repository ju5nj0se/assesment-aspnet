using System.ComponentModel.DataAnnotations;
using JuanJoseHernandez.Constants;

namespace JuanJoseHernandez.Data.Entities;

public class StatusEmployee
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ValidationMessages.StatusRequired)]
    [MaxLength(50, ErrorMessage = ValidationMessages.MaxLength50)]
    [RegularExpression("^(Vacaciones|Activo|Inactivo)$",
        ErrorMessage = ValidationMessages.ValidationStatus.InvalidStatus)]
    public string Status { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}