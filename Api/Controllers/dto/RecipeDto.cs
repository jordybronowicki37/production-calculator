using ProductionCalculator.Core.components.entities;

namespace ProductionCalculator.Api.Controllers.dto;

public class RecipeDto
{
    public Guid Id { get; }
    public string Name { get; }

    public IEnumerable<ThroughPutDto> InputThroughPuts { get; }
    public IEnumerable<ThroughPutDto> OutputThroughPuts { get; }
    
    public RecipeDto(Recipe recipe)
    {
        Id = recipe.Id;
        Name = recipe.Name;
        InputThroughPuts = recipe.InputThroughPuts.Select(t => new ThroughPutDto(t));
        OutputThroughPuts = recipe.OutputThroughPuts.Select(t => new ThroughPutDto(t));
    }
}