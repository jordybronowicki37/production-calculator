using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:Guid}/[controller]")]
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
    public IActionResult GetAll(Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        return Ok(e.Recipes);
    }

    [HttpPost("")]
    public IActionResult Create(DtoRecipe dto, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");
        
        var r = e.GenerateRecipe(dto.Name);

        foreach (var inputThroughPut in dto.InputThroughPuts)
            r.InputThroughPuts.Add(new ThroughPut(e.GetOrGenerateProduct(inputThroughPut.Product.Name), inputThroughPut.Amount));
        
        foreach (var outputThroughPut in dto.OutputThroughPuts)
            r.OutputThroughPuts.Add(new ThroughPut(e.GetOrGenerateProduct(outputThroughPut.Product.Name), outputThroughPut.Amount));
        
        // _context.SaveChanges();
        
        return Ok(r);
    }
    
    [HttpDelete("{id:Guid}")]
    public IActionResult Remove(Guid id, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var e = GetEntityContainer(w.EntityContainerId);
        if (e == null) return NotFound("Entity container is not found");

        var recipe = e.Recipes.FirstOrDefault(r => r.Id == id);
        if (recipe == null) return NotFound("Recipe is not found");
        e.Recipes.Remove(recipe);
        
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
        return _context.Worksheets.Find(filter).FirstOrDefault();
    }
}