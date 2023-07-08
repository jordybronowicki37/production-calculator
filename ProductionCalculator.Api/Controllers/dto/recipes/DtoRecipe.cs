using productionCalculatorLib.components.entities;
using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.recipes;

public class DtoRecipe
{
    public Guid Id { get; }
    public string Name { get; }

    public IEnumerable<DtoThroughPut> InputThroughPuts { get; }
    public IEnumerable<DtoThroughPut> OutputThroughPuts { get; }
    
    public DtoRecipe(Recipe recipe)
    {
        Id = recipe.Id;
        Name = recipe.Name;
        InputThroughPuts = recipe.InputThroughPuts.Select(t => new DtoThroughPut(t));
        OutputThroughPuts = recipe.OutputThroughPuts.Select(t => new DtoThroughPut(t));
    }
}