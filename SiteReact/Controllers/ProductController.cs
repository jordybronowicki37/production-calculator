using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.products;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:Guid}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly DocumentContext _context;
    
    public ProductController(
        ILogger<ProductController> logger, 
        DocumentContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult GetAll(Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        return Ok(e.Products);
    }

    [HttpPost("")]
    public IActionResult Create(DtoProduct dto, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var p = e.GetOrGenerateProduct(dto.Name);
        
        // _context.SaveChanges();
        
        return Ok(p);
    }
    
    [HttpPatch("{name}")]
    public IActionResult Update(string name, Guid worksheetId, DtoProduct dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var p = e.GetProduct(name);
        if (p == null) return NotFound("ProductId is not found");
        p.Name = dto.Name;
        
        // _context.SaveChanges();
        
        return Ok(p);
    }
    
    [HttpDelete("{name}")]
    public IActionResult Remove(string name, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        e.RemoveProduct(name);
        
        // _context.SaveChanges();
        
        return NoContent();
    }
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
    
    private Worksheet? GetWorksheet(Guid id)
    {
        var filter = Builders<Worksheet>.Filter.Eq(w => w.Id, id);
        return _context.Worksheets.Find(filter).FirstOrDefault();    }
}