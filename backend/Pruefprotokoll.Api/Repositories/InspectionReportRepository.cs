using Microsoft.EntityFrameworkCore;
using Pruefprotokoll.Api.Data;
using Pruefprotokoll.Api.Models;

namespace Pruefprotokoll.Api.Repositories;

/// <summary>
/// Kapselt alle Datenbankzugriffe für Prüfprotokolle.
/// </summary>
public class InspectionReportRepository : IInspectionReportRepository
{
    private readonly AppDbContext _dbContext;

    public InspectionReportRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<InspectionReport>> GetAllAsync()
    {
        return _dbContext.InspectionReports
            .OrderByDescending(x => x.InspectionDate)
            .ToListAsync();
    }

    public Task<InspectionReport?> GetByIdAsync(int id)
    {
        return _dbContext.InspectionReports.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<InspectionReport> AddAsync(InspectionReport report)
    {
        _dbContext.InspectionReports.Add(report);
        await _dbContext.SaveChangesAsync();
        return report;
    }

    public async Task<bool> UpdateAsync(InspectionReport report)
    {
        var existingReport = await GetByIdAsync(report.Id);
        if (existingReport is null)
        {
            return false;
        }

        existingReport.ProductName = report.ProductName;
        existingReport.InspectorName = report.InspectorName;
        existingReport.InspectionDate = report.InspectionDate;
        existingReport.ResultStatus = report.ResultStatus;
        existingReport.Comment = report.Comment;

        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var report = await GetByIdAsync(id);
        if (report is null)
        {
            return false;
        }

        _dbContext.InspectionReports.Remove(report);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
