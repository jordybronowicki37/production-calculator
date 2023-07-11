using productionCalculatorLib.components.entities;

namespace SiteReact.Controllers.dto;

public class MachineDto
{
    public Guid Id { get; }
    public string Name { get; }
    public IEnumerable<Guid> Recipes { get; }

    public MachineDto(Machine machine)
    {
        Id = machine.Id;
        Name = machine.Name;
        Recipes = machine.Recipes;
    }
}