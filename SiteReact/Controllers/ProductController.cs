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
    public IActionResult GetAll(int workid)
    {
        return Ok(StaticValues.Get().Worksheet[workid].Products);
    }

    [HttpPost("worksheet/{worksheetId:int}")]
    public IActionResult Create(DtoProduct dto, int workid)
    {
        var p = StaticValues.Get().Worksheet[workid].GetOrGenerateProduct(dto.Name);
        return Ok(p);
    }
    
    [HttpPatch("{id:int}/worksheet/{worksheetId:int}")]
    public IActionResult Create(int id, int workid, DtoProduct dto)
    {
        var p = StaticValues.Get().Worksheet[workid].Products[id];
        p.Name = dto.Name;
        return Ok(p);
    }
}