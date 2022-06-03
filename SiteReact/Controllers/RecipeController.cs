using Microsoft.AspNetCore.Mvc;
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
        _logger.Log(LogLevel.Information, "All recipes gotten");
        return Ok(StaticValues.Get().Recipes);
    }
}