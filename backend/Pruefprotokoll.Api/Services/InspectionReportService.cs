using Pruefprotokoll.Api.Models;
using Pruefprotokoll.Api.Repositories;

namespace Pruefprotokoll.Api.Services;

/// <summary>
/// Enthält fachliche Regeln und trennt Controller von Datenzugriff.
/// </summary>
public class InspectionReportService : IInspectionReportService
{
    private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
    {
        "Bestanden",
        "Mängel",
        "Nicht bestanden"
    };

    private readonly IInspectionReportRepository _repository;

    public InspectionReportService(IInspectionReportRepository repository)
    {
        _repository = repository;
    }

    public Task<List<InspectionReport>> GetAllAsync()
    {
        return _repository.GetAllAsync();
    }

    public Task<InspectionReport?> GetByIdAsync(int id)
    {
        return _repository.GetByIdAsync(id);
    }

    public Task<InspectionReport> CreateAsync(InspectionReport report)
    {
        ValidateReport(report);
        report.Id = 0;
        return _repository.AddAsync(report);
    }

    public Task<bool> UpdateAsync(int id, InspectionReport report)
    {
        ValidateReport(report);
        report.Id = id;
        return _repository.UpdateAsync(report);
    }

    public Task<bool> DeleteAsync(int id)
    {
        return _repository.DeleteAsync(id);
    }

    private static void ValidateReport(InspectionReport report)
    {
        if (string.IsNullOrWhiteSpace(report.ProductName))
        {
            throw new ArgumentException("Produktname ist erforderlich.");
        }

        if (string.IsNullOrWhiteSpace(report.InspectorName))
        {
            throw new ArgumentException("Prüfername ist erforderlich.");
        }

        if (string.IsNullOrWhiteSpace(report.ResultStatus) || !AllowedStatuses.Contains(report.ResultStatus))
        {
            throw new ArgumentException("Ungültiger Prüfstatus.");
        }
    }
}
