using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:long}/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    
    public RecipeController(ILogger<RecipeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll(long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        return Ok(w.Recipes);
    }

    [HttpPost]
    public IActionResult Create(DtoRecipe dto, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var r = w.GenerateRecipe(dto.Name);

        foreach (var inputThroughPut in dto.InputThroughPuts)
            r.InputThroughPuts.Add(new ThroughPut(w.GetOrGenerateProduct(inputThroughPut.Product.Name), inputThroughPut.Amount));
        
        foreach (var outputThroughPut in dto.OutputThroughPuts)
            r.OutputThroughPuts.Add(new ThroughPut(w.GetOrGenerateProduct(outputThroughPut.Product.Name), outputThroughPut.Amount));
        
        return Ok(r);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Remove(int id, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        w.Recipes.RemoveAt(id);
        return NoContent();
    }
    
    private Worksheet? GetWorksheet(long worksheetId)
    {
        return StaticValues.Get().Worksheet.FirstOrDefault(w => w.Id == worksheetId);
    }
}