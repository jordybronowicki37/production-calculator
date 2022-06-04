using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.products;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Controllers.dto.throughputs;
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

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(StaticValues.Get().Recipes);
    }

    [HttpPost]
    public IActionResult Create(DtoRecipe dto)
    {
        var r = new Recipe(dto.Name);
        
        dto.InputThroughPuts.ForEach(t =>
            r.InputThroughPuts.Add(new ThroughPut(SearchProduct(t.Product.Name), t.Amount)));
        
        dto.OutputThroughPuts.ForEach(t =>
            r.OutputThroughPuts.Add(new ThroughPut(SearchProduct(t.Product.Name), t.Amount)));

        StaticValues.Get().Recipes.Add(r);
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Remove(int id)
    {
        StaticValues.Get().Recipes.RemoveAt(id);
        return NoContent();
    }

    private Product SearchProduct(string name)
    {
        var products = StaticValues.Get().Products;
        if (products.Any(p => p.Name == name))
        {
            return products.First(p => p.Name == name);
        }

        var p = new Product(name);
        products.Add(p);
        return p;
    }
}