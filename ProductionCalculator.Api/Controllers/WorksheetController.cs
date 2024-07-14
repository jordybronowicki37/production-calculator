using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.calculator;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("worksheet")]
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
        var ws = GetAllWorksheets();
        return Ok(ws.Select(w => new WorksheetDto(w)));
    }

    [HttpGet("{id:Guid}")]
    public IActionResult Get(Guid id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        return Ok(new WorksheetDto(w));
    }

    [HttpPatch("{id:Guid}")]
    public IActionResult Edit(Guid id, WorksheetCreateDto worksheetCreateDto)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");
        
        w.Name = worksheetCreateDto.Name;
        
        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        var update = Builders<Worksheet>.Update.Set(f => f.Name, w.Name);
        _context.Worksheets.UpdateOne(filter, update);
        
        return Ok(new WorksheetDto(w));
    }
    
    [HttpPost("{id:Guid}/calculate")]
    public IActionResult Calculate(Guid id)
    {
        var w = GetWorksheet(id);
        if (w == null) return NotFound("Worksheet is not found");

        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        new Calculator(w, e).ReCalculateAmounts();

        var filter = Builders<Worksheet>.Filter.Eq(f => f.Id, w.Id);
        _context.Worksheets.ReplaceOne(filter, w);
        
        return Ok(new WorksheetDto(w));
    }
    
    private IEnumerable<Worksheet> GetAllWorksheets()
    {
        var filter = Builders<Worksheet>.Filter.Empty;
        return _context.Worksheets.Find(filter).ToList();
    }
    
    private Worksheet? GetWorksheet(Guid id)
    {
        var filter = Builders<Worksheet>.Filter.Eq(w => w.Id, id);
        return _context.Worksheets.Find(filter).FirstOrDefault();
    }
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
}