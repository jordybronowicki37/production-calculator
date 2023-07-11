using productionCalculatorLib.components.entityContainer;
using SiteReact.Controllers.dto.machines;
using SiteReact.Controllers.dto.products;
using SiteReact.Controllers.dto.recipes;

namespace SiteReact.Controllers.dto.entityContainers;

public class DtoEntityContainer
{
    public Guid Id { get; }
    public string Name { get; }
    public IEnumerable<DtoProduct> Products { get; }
    public IEnumerable<DtoRecipe> Recipes { get; }
    public IEnumerable<DtoMachine> Machines { get; }

    public DtoEntityContainer(EntityContainer entityContainer)
    {
        Id = entityContainer.Id;
        Name = entityContainer.Name;
        Products = entityContainer.Products.Select(p => new DtoProduct(p));
        Recipes = entityContainer.Recipes.Select(r => new DtoRecipe(r));
        Machines = entityContainer.Machines.Select(m => new DtoMachine(m));
    }
}