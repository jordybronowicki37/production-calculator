﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductionCalculator.Api.Controllers.dto;
using ProductionCalculator.Api.Data.DbContexts;
using ProductionCalculator.Core.components.entityContainer;

namespace ProductionCalculator.Api.Controllers;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/entityContainer/{entityContainerId:Guid}/[controller]")]
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
        
        return Ok(e.Products.Select(p => new ProductDto(p)));
    }

    [HttpPost("")]
    public IActionResult Create(ProductCreateDto productCreateDto, Guid entityContainerId)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var p = e.GetOrGenerateProduct(productCreateDto.Name);
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Products, e.Products);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return Ok(new ProductDto(p));
    }
    
    [HttpPatch("{productId:Guid}")]
    public IActionResult Update(Guid productId, Guid entityContainerId, ProductCreateDto productCreateDto)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var p = e.GetProduct(productId);
        if (p == null) return NotFound("ProductId is not found");
        p.Name = productCreateDto.Name;
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Products, e.Products);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return Ok(new ProductDto(p));
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