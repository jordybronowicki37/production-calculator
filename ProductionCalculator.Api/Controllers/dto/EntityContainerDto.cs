using productionCalculatorLib.components.entityContainer;

namespace SiteReact.Controllers.dto;

public class EntityContainerDto
{
    public Guid Id { get; }
    public string Name { get; }
    public IEnumerable<ProductDto> Products { get; }
    public IEnumerable<RecipeDto> Recipes { get; }
    public IEnumerable<MachineDto> Machines { get; }

    public EntityContainerDto(EntityContainer entityContainer)
    {
        Id = entityContainer.Id;
        Name = entityContainer.Name;
        Products = entityContainer.Products.Select(p => new ProductDto(p));
        Recipes = entityContainer.Recipes.Select(r => new RecipeDto(r));
        Machines = entityContainer.Machines.Select(m => new MachineDto(m));
    }
}