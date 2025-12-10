using JuanJoseHernandez.DTOs;

namespace JuanJoseHernandez.Services.Interfaces
{
    public class ImportResult
    {
        public List<string> Errors { get; set; } = new List<string>();
        public int SuccessCount { get; set; }
        public int UpdatedCount { get; set; }
    }

    public interface IUserImportService
    {
        Task<ImportResult> ProcessBatchAsync(List<ExcelDataDto> batch);
    }
}
