using productionCalculatorLib.components.entities;
using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.recipes;

public class DtoRecipe
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";

    public IEnumerable<DtoThroughPut> InputThroughPuts { get; set; }
    public IEnumerable<DtoThroughPut> OutputThroughPuts { get; set; }
    
    public DtoRecipe(Recipe recipe)
    {
        Id = recipe.Id;
        Name = recipe.Name;
        InputThroughPuts = recipe.InputThroughPuts.Select(t => new DtoThroughPut(t));
        OutputThroughPuts = recipe.OutputThroughPuts.Select(t => new DtoThroughPut(t));
    }
}