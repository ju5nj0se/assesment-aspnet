using JuanJoseHernandez.Constants;
using JuanJoseHernandez.Data.Entities;
using JuanJoseHernandez.Data.Repositories.Interfaces;
using JuanJoseHernandez.DTOs;
using JuanJoseHernandez.Services.Interfaces;

namespace JuanJoseHernandez.Services.Implementations
{
    public class UserImportService : IUserImportService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICatalogRepository _catalogRepository;

        public UserImportService(IUserRepository userRepository, ICatalogRepository catalogRepository)
        {
            _userRepository = userRepository;
            _catalogRepository = catalogRepository;
        }

        public async Task<ImportResult> ProcessBatchAsync(List<ExcelDataDto> batch)
        {
            var result = new ImportResult();
            int rowIndexLocal = 0; // Relative index for error reporting

            foreach (var dto in batch)
            {
                rowIndexLocal++;
                try
                {
                    // 1. Basic Validation
                    if (string.IsNullOrWhiteSpace(dto.Email))
                    {
                        result.Errors.Add($"Usuario sin Email: es obligatorio.");
                        continue;
                    }

                    // 2. Resolve Foreign Keys
                    var department = await _catalogRepository.GetDepartmentByNameAsync(dto.Departamento);
                    if (department == null) { result.Errors.Add($"Email {dto.Email}: Departamento '{dto.Departamento}' inválido."); continue; }

                    var degree = await _catalogRepository.GetDegreeByNameAsync(dto.Cargo);
                    if (degree == null) { result.Errors.Add($"Email {dto.Email}: Cargo '{dto.Cargo}' inválido."); continue; }

                    var educationLevel = await _catalogRepository.GetEducationLevelByNameAsync(dto.NivelEducativo);
                    if (educationLevel == null) { result.Errors.Add($"Email {dto.Email}: Nivel '{dto.NivelEducativo}' inválido."); continue; }

                    var status = await _catalogRepository.GetStatusByNameAsync(dto.Estado);
                    if (status == null) { result.Errors.Add($"Email {dto.Email}: Estado '{dto.Estado}' inválido."); continue; }

                    // 3. Check Existence
                    var user = await _userRepository.GetByEmailAsync(dto.Email);
                    bool isUpdate = user != null;

                    // Validate generic document uniqueness
                    var userByDoc = await _userRepository.GetByDocumentAsync(dto.Documento);
                    if (userByDoc != null)
                    {
                        // If creating, duplicate. If updating, duplicate if ID mismatch.
                        if (!isUpdate || (isUpdate && userByDoc.Id != user.Id))
                        {
                             result.Errors.Add($"Email {dto.Email}: Documento '{dto.Documento}' ya registrado al usuario {userByDoc.Email}.");
                             continue;
                        }
                    }

                    if (!isUpdate)
                    {
                        user = new User
                        {
                            UserName = dto.Email,
                            Email = dto.Email,
                            SecurityStamp = Guid.NewGuid().ToString()
                        };
                    }

                    // 4. Map Properties
                    user.Document = dto.Documento;
                    user.Names = dto.Nombres;
                    user.LastNames = dto.Apellidos;
                    user.BirthDate = DateOnly.FromDateTime(dto.FechaNacimiento); // New Attribute
                    user.Direction = dto.Direccion;
                    user.Telephone = dto.Telefono;
                    user.Salary = dto.Salario; // Using decimal from DTO
                    
                    // Ensure Date is DateOnly
                    user.DateEntry = DateOnly.FromDateTime(dto.FechaIngreso);
                    
                    user.Profile = dto.PerfilProfesional;
                    user.Department = department;
                    user.Degree = degree;
                    user.EducationLevel = educationLevel;
                    user.Status = status;

                    // 5. Persist
                    if (isUpdate)
                    {
                        var updateResult = await _userRepository.UpdateAsync(user);
                        if (!updateResult.Succeeded)
                        {
                            result.Errors.Add($"Email {dto.Email}: Error al actualizar - {string.Join(", ", updateResult.Errors.Select(e => e.Description))}");
                        }
                        else
                        {
                            result.UpdatedCount++;
                        }
                    }
                    else
                    {
                        var createResult = await _userRepository.CreateAsync(user);
                        if (createResult.Succeeded)
                        {
                            await _userRepository.AddToRoleAsync(user, Roles.User);
                            result.SuccessCount++;
                        }
                        else
                        {
                            result.Errors.Add($"Email {dto.Email}: Error al crear - {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Errors.Add($"Error procesando {dto.Email}: {ex.Message}");
                }
            }

            return result;
        }
    }
}
