using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.products;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    
    public RecipeController(ILogger<RecipeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("worksheet/{worksheetId:int}")]
    public IActionResult GetAll(int worksheetId)
    {
        return Ok(StaticValues.Get().Worksheet[worksheetId].Recipes);
    }

    [HttpPost("worksheet/{worksheetId:int}")]
    public IActionResult Create(DtoRecipe dto, int worksheetId)
    {
        var worksheet = StaticValues.Get().Worksheet[worksheetId];
        var r = worksheet.GenerateRecipe(dto.Name);
        
        dto.InputThroughPuts.ForEach(t =>
            r.InputThroughPuts.Add(new ThroughPut(worksheet.GetOrGenerateProduct(t.Product.Name), t.Amount)));
        
        dto.OutputThroughPuts.ForEach(t =>
            r.OutputThroughPuts.Add(new ThroughPut(worksheet.GetOrGenerateProduct(t.Product.Name), t.Amount)));

        return Ok();
    }
    
    [HttpDelete("{id:int}/worksheet/{worksheetId:int}")]
    public IActionResult Remove(int id, int worksheetId)
    {
        StaticValues.Get().Worksheet[worksheetId].Recipes.RemoveAt(id);
        return NoContent();
    }
}