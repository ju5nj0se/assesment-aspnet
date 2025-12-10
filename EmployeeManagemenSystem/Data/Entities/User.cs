using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JuanJoseHernandez.Constants;
using Microsoft.AspNetCore.Identity;

namespace JuanJoseHernandez.Data.Entities;

public class User : IdentityUser
{
    [Required(ErrorMessage = ValidationMessages.DocumentRequired)]
    [MaxLength(20, ErrorMessage = ValidationMessages.MaxLength20)]
    public string Document { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength100)]
    public string Names { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationMessages.NameRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength100)]
    public string LastNames { get; set; } = string.Empty;
    
    [Required(ErrorMessage = ValidationMessages.DateRequired)]
    [DataType(DataType.Date)]
    public DateOnly BirthDate { get; set; }

    [Required(ErrorMessage = ValidationMessages.AddressRequired)]
    [MaxLength(200, ErrorMessage = ValidationMessages.MaxLength200)]
    public string Direction { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationMessages.PhoneRequired)]
    [MaxLength(20, ErrorMessage = ValidationMessages.MaxLength20)]
    [Phone(ErrorMessage = ValidationMessages.InvalidPhoneFormat)]
    public string Telephone { get; set; } = string.Empty;

    [Required(ErrorMessage = ValidationMessages.SalaryRequired)]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 999999999.99, ErrorMessage = ValidationMessages.PositiveValueRequired)]
    public decimal Salary { get; set; }

    [Required(ErrorMessage = ValidationMessages.DateRequired)]
    [DataType(DataType.Date)]
    public DateOnly DateEntry { get; set; }

    [Required(ErrorMessage = ValidationMessages.ProfileRequired)]
    [MaxLength(100, ErrorMessage = ValidationMessages.MaxLength100)]
    public string Profile { get; set; } = string.Empty;

    // Relaciones (Foreign Keys)
    
    public int? DegreeId { get; set; }
    
    [ForeignKey(nameof(DegreeId))]
    public Degree? Degree { get; set; }

    public int? StatusId { get; set; }
    
    [ForeignKey(nameof(StatusId))]
    public StatusEmployee? Status { get; set; }
    
    public int? EducationLevelId { get; set; }
    
    [ForeignKey(nameof(EducationLevelId))]
    public EducationLevel? EducationLevel { get; set; }
    
    public int? DepartmentId { get; set; }
    
    [ForeignKey(nameof(DepartmentId))]
    public Department? Department { get; set; }
}