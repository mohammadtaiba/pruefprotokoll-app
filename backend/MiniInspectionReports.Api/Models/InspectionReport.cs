using System.ComponentModel.DataAnnotations;

namespace MiniInspectionReports.Api.Models;

/// <summary>
/// Zentrales Datenmodell für ein Prüfprotokoll.
/// </summary>
public class InspectionReport
{
    public int Id { get; set; }

    [Required]
    [MaxLength(120)]
    public string ProductName { get; set; } = string.Empty;

    [Required]
    [MaxLength(120)]
    public string InspectorName { get; set; } = string.Empty;

    [Required]
    public DateTime InspectionDate { get; set; }

    [Required]
    [MaxLength(30)]
    public string ResultStatus { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Comment { get; set; }
}
