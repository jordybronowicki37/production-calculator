using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.worksheet;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new DtoWorksheetSmall[] {new (StaticValues.Get().Worksheet)});
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        return Ok(new DtoWorksheet(StaticValues.Get().Worksheet));
    }

    [HttpPost]
    public IActionResult CreateNew()
    {
        var w = new Worksheet();
        return Ok(new DtoWorksheet(w));
    }
}