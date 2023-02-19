using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.project;
using SiteReact.Controllers.dto.products;
using SiteReact.Controllers.dto.recipes;

namespace SiteReact.Controllers.dto.projects;

public class DtoProject
{
    public Guid Id { get; }
    public string Name { get; }
    public int AmountWorksheets { get; }
    public Guid EntityContainerId { get; }
    public IEnumerable<DtoProduct> Products { get; }
    public IEnumerable<DtoRecipe> Recipes { get; }

    public DtoProject(Project project, EntityContainer entityContainer)
    {
        Id = project.Id;
        Name = project.Name;
        AmountWorksheets = project.Worksheets.Count;
        EntityContainerId = project.EntityContainerId;
        Products = entityContainer.Products.Select(p => new DtoProduct(p));
        Recipes = entityContainer.Recipes.Select(r => new DtoRecipe(r));
    }
}
