using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.products;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(StaticValues.Get().Products);
    }

    [HttpPost]
    public IActionResult Create(DtoProduct dto)
    {
        var p = new Product(dto.Name);
        StaticValues.Get().Products.Add(p);
        return Ok(p);
    }
    
    [HttpPatch("{id:int}")]
    public IActionResult Create(int id, DtoProduct dto)
    {
        var p = StaticValues.Get().Products[id];
        p.Name = dto.Name;
        return Ok(p);
    }
}