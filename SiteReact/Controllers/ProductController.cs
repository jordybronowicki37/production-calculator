using Microsoft.AspNetCore.Mvc;
using SiteReact.Controllers.dto.products;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    
    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [HttpGet("worksheet/{worksheetId:int}")]
    public IActionResult GetAll(int worksheetId)
    {
        return Ok(StaticValues.Get().Worksheet[worksheetId].Products);
    }

    [HttpPost("worksheet/{worksheetId:int}")]
    public IActionResult Create(DtoProduct dto, int worksheetId)
    {
        var p = StaticValues.Get().Worksheet[worksheetId].GetOrGenerateProduct(dto.Name);
        return Ok(p);
    }
    
    [HttpPatch("{id:int}/worksheet/{worksheetId:int}")]
    public IActionResult Create(int id, int worksheetId, DtoProduct dto)
    {
        var p = StaticValues.Get().Worksheet[worksheetId].Products[id];
        p.Name = dto.Name;
        return Ok(p);
    }
}