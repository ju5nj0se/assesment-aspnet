using System.ComponentModel.DataAnnotations;
using JuanJoseHernandez.Constants;

namespace JuanJoseHernandez.Data.Entities;

public class Degree
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength100)]
    [RegularExpression("^(Ingeniero|Soporte Técnico|Analista|Coordinador|Desarrollador|Auxiliar|Administrador)$",
        ErrorMessage = ValidationMessages.ValidationDegree.InvalidDegree)]
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}