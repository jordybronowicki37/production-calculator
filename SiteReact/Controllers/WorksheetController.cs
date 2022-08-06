using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.calculator;
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
        return Ok(StaticValues.Get().Worksheet.Select(w => new DtoWorksheetSmall(w)));
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var w = StaticValues.Get().Worksheet[id];
        CalculatorLimit.ReCalculateAmounts(w);
        return Ok(new DtoWorksheet(w));
    }

    [HttpPost]
    public IActionResult CreateNew(DtoWorksheetCreate dto)
    {
        var w = new Worksheet(){Name = dto.Name};
        StaticValues.Get().Worksheet.Add(w);
        return Ok(new DtoWorksheet(w));
    }

    [HttpPatch("{id:int}")]
    public IActionResult Edit(int id, DtoWorksheetCreate dto)
    {
        var w = StaticValues.Get().Worksheet[id];
        w.Name = dto.Name;
        return Ok();
    }
}