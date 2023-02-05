using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using SiteReact.Controllers.dto.products;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("entityContainer/{entityContainerId:Guid}/[controller]")]
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
    public IActionResult GetAll(Guid entityContainerId)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        return Ok(e.Products);
    }

    [HttpPost("")]
    public IActionResult Create(DtoProduct dto, Guid entityContainerId)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var p = e.GetOrGenerateProduct(dto.Name);
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Products, e.Products);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return Ok(p);
    }
    
    [HttpPatch("{productId:Guid}")]
    public IActionResult Update(Guid productId, Guid entityContainerId, DtoProduct dto)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var p = e.GetProduct(productId);
        if (p == null) return NotFound("ProductId is not found");
        p.Name = dto.Name;
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Products, e.Products);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return Ok(p);
    }
    
    [HttpDelete("{productId:Guid}")]
    public IActionResult Remove(Guid productId, Guid entityContainerId)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        e.RemoveProduct(productId);
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Products, e.Products);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return NoContent();
    }
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
}