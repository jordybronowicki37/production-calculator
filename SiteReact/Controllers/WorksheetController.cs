using Microsoft.AspNetCore.Mvc;
using SiteReact.Controllers.dto.worksheets;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class WorksheetController : ControllerBase
{
    private readonly ILogger<WorksheetController> _logger;
    
    public WorksheetController(ILogger<WorksheetController> logger)
    {
        _logger = logger;
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        _logger.Log(LogLevel.Information, "Worksheet gotten");
        return Ok(new DtoWorksheet(StaticValues.Get().Worksheet));
    }
}