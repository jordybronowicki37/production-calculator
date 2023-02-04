using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
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
        
        return Ok(w.EntityContainer.Recipes);
    }

    [HttpPost("")]
    public IActionResult Create(DtoRecipe dto, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var r = w.EntityContainer.GenerateRecipe(dto.Name);

        foreach (var inputThroughPut in dto.InputThroughPuts)
            r.InputThroughPuts.Add(new ThroughPut(w.EntityContainer.GetOrGenerateProduct(inputThroughPut.Product.Name), inputThroughPut.Amount));
        
        foreach (var outputThroughPut in dto.OutputThroughPuts)
            r.OutputThroughPuts.Add(new ThroughPut(w.EntityContainer.GetOrGenerateProduct(outputThroughPut.Product.Name), outputThroughPut.Amount));
        
        // _context.SaveChanges();
        
        return Ok(r);
    }
    
    [HttpDelete("{id:Guid}")]
    public IActionResult Remove(Guid id, Guid worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");

        var recipe = w.EntityContainer.Recipes.FirstOrDefault(r => r.Id == id);
        if (recipe == null) return NotFound("Recipe is not found");
        w.EntityContainer.Recipes.Remove(recipe);
        
        // _context.SaveChanges();
        
        return NoContent();
    }
    
    private Worksheet? GetWorksheet(Guid worksheetId)
    {
        var filter = Builders<Worksheet>.Filter.Eq(w => w.Id, worksheetId);
        return _context.Worksheets.Find(filter).FirstOrDefault();
    }
}