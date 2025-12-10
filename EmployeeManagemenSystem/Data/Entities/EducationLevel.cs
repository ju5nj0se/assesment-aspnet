using System.ComponentModel.DataAnnotations;
using JuanJoseHernandez.Constants;

namespace JuanJoseHernandez.Data.Entities;

public class EducationLevel
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength100)]
    [RegularExpression("^(Profesional|Tecnólogo|Maestría|Especialización|Técnico)$",
        ErrorMessage = ValidationMessages.ValidationEducationLevel.InvalidEducationLevel)]
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}