using Microsoft.AspNetCore.Mvc;
using Pruefprotokoll.Api.Models;
using Pruefprotokoll.Api.Services;

namespace Pruefprotokoll.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InspectionReportsController : ControllerBase
{
    private readonly IInspectionReportService _service;

    public InspectionReportsController(IInspectionReportService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<InspectionReport>>> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InspectionReport>> GetById(int id)
    {
        var report = await _service.GetByIdAsync(id);
        return report is null ? NotFound() : Ok(report);
    }

    [HttpPost]
    public async Task<ActionResult<InspectionReport>> Create(InspectionReport report)
    {
        try
        {
            var createdReport = await _service.CreateAsync(report);
            return CreatedAtAction(nameof(GetById), new { id = createdReport.Id }, createdReport);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, InspectionReport report)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, report);
            return updated ? NoContent() : NotFound();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
