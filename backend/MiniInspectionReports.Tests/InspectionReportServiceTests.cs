using Microsoft.EntityFrameworkCore;
using MiniInspectionReports.Api.Data;
using MiniInspectionReports.Api.Models;
using MiniInspectionReports.Api.Repositories;
using MiniInspectionReports.Api.Services;
using Xunit;

namespace MiniInspectionReports.Tests;

public class InspectionReportServiceTests
{
    [Fact]
    public async Task CreateAsync_WithValidReport_PersistsReport()
    {
        await using var dbContext = CreateDbContext();
        var service = CreateService(dbContext);

        var report = new InspectionReport
        {
            ProductName = "Drehmomentschlüssel DT-10",
            InspectorName = "Test Prüfer",
            InspectionDate = new DateTime(2026, 5, 10),
            ResultStatus = "Bestanden",
            Comment = "Messwerte im Sollbereich."
        };

        var created = await service.CreateAsync(report);
        var loaded = await service.GetByIdAsync(created.Id);

        Assert.NotNull(loaded);
        Assert.Equal("Drehmomentschlüssel DT-10", loaded!.ProductName);
        Assert.Equal("Bestanden", loaded.ResultStatus);
    }

    [Fact]
    public async Task UpdateAsync_WhenReportExists_UpdatesReport()
    {
        await using var dbContext = CreateDbContext();
        var service = CreateService(dbContext);

        var created = await service.CreateAsync(new InspectionReport
        {
            ProductName = "Sensor S-100",
            InspectorName = "Test Prüfer",
            InspectionDate = new DateTime(2026, 5, 11),
            ResultStatus = "Mängel",
            Comment = "Kalibrierung prüfen."
        });

        var success = await service.UpdateAsync(created.Id, new InspectionReport
        {
            ProductName = "Sensor S-100 geprüft",
            InspectorName = "Test Prüfer 2",
            InspectionDate = new DateTime(2026, 5, 12),
            ResultStatus = "Nicht bestanden",
            Comment = "Grenzwert überschritten."
        });

        var updated = await service.GetByIdAsync(created.Id);

        Assert.True(success);
        Assert.NotNull(updated);
        Assert.Equal("Sensor S-100 geprüft", updated!.ProductName);
        Assert.Equal("Nicht bestanden", updated.ResultStatus);
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var dbContext = new AppDbContext(options);
        dbContext.Database.EnsureCreated();
        return dbContext;
    }

    private static InspectionReportService CreateService(AppDbContext dbContext)
    {
        var repository = new InspectionReportRepository(dbContext);
        return new InspectionReportService(repository);
    }
}
