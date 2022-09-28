﻿using Microsoft.AspNetCore.Mvc;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Data.DbContexts;

namespace SiteReact.Controllers;

[ApiController]
[Route("worksheet/{worksheetId:long}/[controller]")]
public class RecipeController : ControllerBase
{
    private readonly ILogger<RecipeController> _logger;
    private readonly ProjectContext _context;
    
    public RecipeController(
        ILogger<RecipeController> logger, 
        ProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll(long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        return Ok(w.EntityContainer.Recipes);
    }

    [HttpPost]
    public IActionResult Create(DtoRecipe dto, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");
        
        var r = w.EntityContainer.GenerateRecipe(dto.Name);

        foreach (var inputThroughPut in dto.InputThroughPuts)
            r.InputThroughPuts.Add(new ThroughPut(w.EntityContainer.GetOrGenerateProduct(inputThroughPut.Product.Name), inputThroughPut.Amount));
        
        foreach (var outputThroughPut in dto.OutputThroughPuts)
            r.OutputThroughPuts.Add(new ThroughPut(w.EntityContainer.GetOrGenerateProduct(outputThroughPut.Product.Name), outputThroughPut.Amount));
        
        _context.SaveChanges();
        
        return Ok(r);
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult Remove(int id, long worksheetId)
    {
        var w = GetWorksheet(worksheetId);
        if (w == null) return NotFound("Worksheet is not found");

        var recipe = w.EntityContainer.Recipes.FirstOrDefault(r => r.Id == id);
        w.EntityContainer.Recipes.Remove(recipe);
        
        _context.SaveChanges();
        
        return NoContent();
    }
    
    private Worksheet? GetWorksheet(long worksheetId)
    {
        return _context.Worksheets.FirstOrDefault(w => w.Id == worksheetId);
    }
}