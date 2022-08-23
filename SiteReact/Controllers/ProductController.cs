using Microsoft.AspNetCore.Mvc;
using SiteReact.Controllers.dto.products;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:int}/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    
    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll(int worksheetId)
    {
        return Ok(StaticValues.Get().Worksheet[worksheetId].Products);
    }

    [HttpPost]
    public IActionResult Create(DtoProduct dto, int worksheetId)
    {
        var p = StaticValues.Get().Worksheet[worksheetId].GetOrGenerateProduct(dto.Name);
        return Ok(p);
    }
    
    [HttpPatch("{name}")]
    public IActionResult Update(string name, int worksheetId, DtoProduct dto)
    {
        var p = StaticValues.Get().Worksheet[worksheetId].Products.FirstOrDefault(p => p.Name == name);
        if (p == null) return NotFound("Product is not found");
        p.Name = dto.Name;
        return Ok(p);
    }
    
    [HttpDelete("{name}")]
    public IActionResult Remove(string name, int worksheetId)
    {
        try
        {
            StaticValues.Get().Worksheet[worksheetId].RemoveProduct(name);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound();
        }
    }
}