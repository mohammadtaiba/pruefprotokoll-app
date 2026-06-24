using Pruefprotokoll.Api.Models;

namespace Pruefprotokoll.Api.Services;

public interface IInspectionReportService
{
    Task<List<InspectionReport>> GetAllAsync();
    Task<InspectionReport?> GetByIdAsync(int id);
    Task<InspectionReport> CreateAsync(InspectionReport report);
    Task<bool> UpdateAsync(int id, InspectionReport report);
    Task<bool> DeleteAsync(int id);
}
