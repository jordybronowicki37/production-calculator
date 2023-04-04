using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.products;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{entityContainerId:Guid}/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly DocumentContext _context;
    
    public RecipeController(
        ILogger<RecipeController> logger, 
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
        
        return Ok(e.Recipes);
    }

    [HttpPost("")]
    public IActionResult Create(DtoRecipe dto, Guid entityContainerId)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");

        if (!dto.Machines.Any()) return BadRequest("Recipe must contain at least one machine");
        var machines = e.GetMachines(dto.Machines).ToList();
        var machine1 = machines[0];
        machines.RemoveAt(0);
        
        var r = e.GenerateRecipe(dto.Name, machine1, machines.ToArray());

        foreach (var inputThroughPut in dto.InputThroughPuts)
            r.InputThroughPuts.Add(new ThroughPut(e.GetOrGenerateProduct(inputThroughPut.Product.Name), inputThroughPut.Amount));
        
        foreach (var outputThroughPut in dto.OutputThroughPuts)
            r.OutputThroughPuts.Add(new ThroughPut(e.GetOrGenerateProduct(outputThroughPut.Product.Name), outputThroughPut.Amount));
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Recipes, e.Recipes);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return Ok(r);
    }
    
    [HttpDelete("{recipeId:Guid}")]
    public IActionResult Remove(Guid recipeId, Guid entityContainerId)
    {
        var e = GetEntityContainer(entityContainerId);
        if (e == null) return NotFound("Entity container is not found");

        e.RemoveRecipe(recipeId);
        
        var filter = Builders<EntityContainer>.Filter.Eq(f => f.Id, e.Id);
        var update = Builders<EntityContainer>.Update.Set(f => f.Recipes, e.Recipes);
        _context.EntityContainers.UpdateOne(filter, update);
        
        return NoContent();
    }
    
    private EntityContainer? GetEntityContainer(Guid id)
    {
        var filter = Builders<EntityContainer>.Filter.Eq(w => w.Id, id);
        return _context.EntityContainers.Find(filter).FirstOrDefault();
    }
}