using MiniInspectionReports.Api.Models;

namespace MiniInspectionReports.Api.Repositories;

public interface IInspectionReportRepository
{
    Task<List<InspectionReport>> GetAllAsync();
    Task<InspectionReport?> GetByIdAsync(int id);
    Task<InspectionReport> AddAsync(InspectionReport report);
    Task<bool> UpdateAsync(InspectionReport report);
    Task<bool> DeleteAsync(int id);
}
