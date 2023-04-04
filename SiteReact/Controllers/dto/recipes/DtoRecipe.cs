using SiteReact.Controllers.dto.throughputs;

namespace SiteReact.Controllers.dto.recipes;

public class DtoRecipe
{
    public string Name { get; set; } = "";

    public IEnumerable<Guid> Machines { get; set; } = null!;

    public IEnumerable<DtoThroughPut> InputThroughPuts { get; set; } = null!;
    public IEnumerable<DtoThroughPut> OutputThroughPuts { get; set; } = null!;
}