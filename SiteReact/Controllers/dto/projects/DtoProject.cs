using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.project;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.machines;
using SiteReact.Controllers.dto.products;
using SiteReact.Controllers.dto.recipes;
using SiteReact.Controllers.dto.worksheets;

namespace SiteReact.Controllers.dto.projects;

public class DtoProject
{
    public Guid Id { get; }
    public string Name { get; }
    public Guid EntityContainerId { get; }
    public IEnumerable<DtoWorksheetSmall> Worksheets { get; }
    public IEnumerable<DtoProduct> Products { get; }
    public IEnumerable<DtoRecipe> Recipes { get; }
    public IEnumerable<DtoMachine> Machines { get; }

    public DtoProject(Project project, EntityContainer entityContainer, IEnumerable<Worksheet> worksheets)
    {
        Id = project.Id;
        Name = project.Name;
        EntityContainerId = project.EntityContainerId;
        Worksheets = worksheets.Select(w => new DtoWorksheetSmall(w));
        Products = entityContainer.Products.Select(p => new DtoProduct(p));
        Recipes = entityContainer.Recipes.Select(r => new DtoRecipe(r));
        Machines = entityContainer.Machines.Select(m => new DtoMachine(m));
    }
}
