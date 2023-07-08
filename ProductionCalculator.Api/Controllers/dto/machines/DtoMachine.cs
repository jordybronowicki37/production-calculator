using productionCalculatorLib.components.products;

namespace SiteReact.Controllers.dto.machines;

public class DtoMachine
{
    public Guid Id { get; }
    public string Name { get; }
    public IEnumerable<Guid> Recipes { get; }

    public DtoMachine(Machine machine)
    {
        Id = machine.Id;
        Name = machine.Name;
        Recipes = machine.Recipes;
    }
}