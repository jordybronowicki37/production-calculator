using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.products;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:long}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    
    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll(long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        return Ok(w.Products);
    }

    [HttpPost]
    public IActionResult Create(DtoProduct dto, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var p = w.GetOrGenerateProduct(dto.Name);
        return Ok(p);
    }
    
    [HttpPatch("{name}")]
    public IActionResult Update(string name, long worksheetId, DtoProduct dto)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var p = w.GetProduct(name);
        if (p == null) return NotFound("Product is not found");
        p.Name = dto.Name;
        return Ok(p);
    }
    
    [HttpDelete("{name}")]
    public IActionResult Remove(string name, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        w.RemoveProduct(name);
        return NoContent();
    }
    
    private Worksheet? GetWorksheet(long worksheetId)
    {
        return StaticValues.Get().Worksheet.FirstOrDefault(w => w.Id == worksheetId);
    }
}