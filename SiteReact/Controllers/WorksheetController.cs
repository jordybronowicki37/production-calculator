using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.calculator;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.worksheets;
using SiteReact.Data.DbContexts;
using SiteReact.Data.GameDataPresets;
using SiteReact.Data.Initializers;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class WorksheetController : ControllerBase
{
    private readonly ILogger<WorksheetController> _logger;
    private readonly DocumentContext _context;
    
    public WorksheetController(
        ILogger<WorksheetController> logger, 
        DocumentContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(GetAllWorksheets().Select(w => new DtoWorksheetSmall(w)));
    }

    [HttpGet("{id:Guid}")]
    public IActionResult Get(Guid id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        if (w.Nodes.Count != 0) CalculatorLimit.ReCalculateAmounts(w);
        return Ok(new DtoWorksheet(w));
    }

    [HttpPost("")]
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

        var testw = TestDataInitializer.InitializeSimpleOneWay();
        
        _context.Worksheets.InsertOne(testw);
        
        return Ok(new DtoWorksheet(testw));
    }

    [HttpPatch("{id:Guid}")]
    public IActionResult Edit(Guid id, DtoWorksheetCreate dto)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        w.Name = dto.Name;
        
        _context.Worksheets.InsertOne(w);
        
        return Ok(new DtoWorksheet(w));
    }
    
    [HttpPost("{id:Guid}/calculate")]
    public IActionResult Calculate(Guid id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        CalculatorLimit.ReCalculateAmounts(w);
        
        _context.Worksheets.InsertOne(w);
        
        return Ok(new DtoWorksheet(w));
    }
    
    private IEnumerable<Worksheet> GetAllWorksheets()
    {
        var filter = Builders<Worksheet>.Filter.Empty;
        return _context.Worksheets.Find(filter).ToList();
    }
    
    private Worksheet? GetWorksheet(Guid worksheetId)
    {
        var filter = Builders<Worksheet>.Filter.Eq(w => w.Id, worksheetId);
        return _context.Worksheets.Find(filter).FirstOrDefault();
    }
}