using Microsoft.EntityFrameworkCore;
using MiniInspectionReports.Api.Models;

namespace MiniInspectionReports.Api.Data;

/// <summary>
/// Entity-Framework-Core-Kontext für Prüfprotokolle.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<InspectionReport> InspectionReports => Set<InspectionReport>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<InspectionReport>(entity =>
        {
            entity.Property(x => x.ProductName).IsRequired().HasMaxLength(120);
            entity.Property(x => x.InspectorName).IsRequired().HasMaxLength(120);
            entity.Property(x => x.InspectionDate).HasColumnType("timestamp without time zone");
            entity.Property(x => x.ResultStatus).IsRequired().HasMaxLength(30);
            entity.Property(x => x.Comment).HasMaxLength(1000);

            // Zwei feste Seed-Datensätze für Demo und Interview-Präsentation.
            entity.HasData(
                new InspectionReport
                {
                    Id = 1,
                    ProductName = "Hydraulikpumpe HP-200",
                    InspectorName = "Anna Müller",
                    InspectionDate = new DateTime(2026, 5, 1),
                    ResultStatus = "Bestanden",
                    Comment = "Keine Auffälligkeiten festgestellt."
                },
                new InspectionReport
                {
                    Id = 2,
                    ProductName = "Steuergerät SG-42",
                    InspectorName = "Max Schneider",
                    InspectionDate = new DateTime(2026, 5, 3),
                    ResultStatus = "Mängel",
                    Comment = "Gehäuse leicht beschädigt, Funktion gegeben."
                }
            );
        });
    }
}
