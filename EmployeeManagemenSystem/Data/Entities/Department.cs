using System.ComponentModel.DataAnnotations;
using JuanJoseHernandez.Constants;

namespace JuanJoseHernandez.Data.Entities;

public class Department
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength100)]
    [RegularExpression("^(Logística|Marketing|Recursos Humanos|Operaciones|Tecnología|Contabilidad|Ventas)$",
        ErrorMessage = ValidationMessages.ValidationDepartment.InvalidDepartment)]
    public string Name { get; set; } = string.Empty;

    public ICollection<User> Users { get; set; } = new List<User>();
}