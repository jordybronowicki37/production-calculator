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

    [HttpGet("{id:long}")]
    public IActionResult Get(long id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        if (w.Nodes.Count != 0) CalculatorLimit.ReCalculateAmounts(w);
        return Ok(new DtoWorksheet(w));
    }

    [HttpPost]
    public IActionResult CreateNew(DtoWorksheetCreate dto)
    {
        var w = new Worksheet(){Name = dto.Name};
        StaticValues.Get().Worksheet.Add(w);
        return Ok(new DtoWorksheet(w));
    }

    [HttpPatch("{id:long}")]
    public IActionResult Edit(long id, DtoWorksheetCreate dto)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        w.Name = dto.Name;
        return Ok();
    }
    
    [HttpPost("{id:long}/calculate")]
    public IActionResult Calculate(long id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        CalculatorLimit.ReCalculateAmounts(w);
        return Ok(new DtoWorksheet(w));
    }
    
    private Worksheet? GetWorksheet(long worksheetId)
    {
        return StaticValues.Get().Worksheet.FirstOrDefault(w => w.Id == worksheetId);
    }
}