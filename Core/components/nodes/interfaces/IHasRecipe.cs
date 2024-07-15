using ProductionCalculator.Core.components.targets;

namespace ProductionCalculator.Core.components.nodes.interfaces;

public interface IHasRecipe: INode
{
    Guid RecipeId { get; set; }
    Guid MachineId { get; set; }
    PowerUp? PowerUp { get; set; }
}
