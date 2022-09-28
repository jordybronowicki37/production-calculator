using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.calculator;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.worksheets;
using SiteReact.Data.DbContexts;
using SiteReact.Data.GameDataPresets;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class WorksheetController : ControllerBase
{
    private readonly ILogger<WorksheetController> _logger;
    private readonly ProjectContext _context;
    
    public WorksheetController(
        ILogger<WorksheetController> logger, 
        ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_context.Worksheets.Select(w => new DtoWorksheetSmall(w)));
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

        switch (dto.DataPreset)
        {
            case "":
            case "none":
                break;
            case "dysonSphereProgram":
                DSPData.addData(w);
                break;
            case "satisfactoryEarlyAccess":
                SatisfactoryData.addData(w);
                break;
            case "satisfactoryExperimental":
                SatisfactoryExperimentalData.addData(w);
                break;
            case "satisfactoryFICSMAS":
                SatisfactoryFicsMasData.addData(w);
                break;
            default:
                return NotFound("Data preset is not found");
        }
        
        _context.Worksheets.Add(w);
        
        _context.SaveChanges();
        
        return Ok(new DtoWorksheet(w));
    }

    [HttpPatch("{id:long}")]
    public IActionResult Edit(long id, DtoWorksheetCreate dto)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        w.Name = dto.Name;
        
        _context.SaveChanges();
        
        return Ok(new DtoWorksheet(w));
    }
    
    [HttpPost("{id:long}/calculate")]
    public IActionResult Calculate(long id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        CalculatorLimit.ReCalculateAmounts(w);
        
        _context.SaveChanges();
        
        return Ok(new DtoWorksheet(w));
    }
    
    private Worksheet? GetWorksheet(long worksheetId)
    {
        return _context.Worksheets.FirstOrDefault(w => w.Id == worksheetId);
    }
}