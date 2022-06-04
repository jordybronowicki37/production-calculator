using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.recipes;

public class DtoRecipe
{
    public string Name { get; set; }

    public List<DtoThroughPut> InputThroughPuts { get; set; }
    public List<DtoThroughPut> OutputThroughPuts { get; set; }
}