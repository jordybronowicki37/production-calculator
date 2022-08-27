using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.products;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Data;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:int}/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    
    public RecipeController(ILogger<RecipeController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetAll(int worksheetId)
    {
        return Ok(StaticValues.Get().Worksheet[worksheetId].Recipes);
    }

    [HttpPost]
    public IActionResult Create(DtoRecipe dto, int worksheetId)
    {
        var worksheet = StaticValues.Get().Worksheet[worksheetId];
        var r = worksheet.GenerateRecipe(dto.Name);

        foreach (var inputThroughPut in dto.InputThroughPuts)
            r.InputThroughPuts.Add(new ThroughPut(worksheet.GetOrGenerateProduct(inputThroughPut.Product.Name), inputThroughPut.Amount));
        
        foreach (var outputThroughPut in dto.OutputThroughPuts)
            r.OutputThroughPuts.Add(new ThroughPut(worksheet.GetOrGenerateProduct(outputThroughPut.Product.Name), outputThroughPut.Amount));
        
        return Ok(r);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Remove(int id, int worksheetId)
    {
        StaticValues.Get().Worksheet[worksheetId].Recipes.RemoveAt(id);
        return NoContent();
    }
}