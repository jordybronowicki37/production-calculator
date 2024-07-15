using ProductionCalculator.Core.components.nodes.abstractions;
using ProductionCalculator.Core.components.nodes.interfaces;
using ProductionCalculator.Core.components.targets;

namespace ProductionCalculator.Core.components.nodes.nodeTypes;

public class ProductionNode: ANode, INodeInOut, IHasRecipe
{
    public Guid RecipeId { get; set; }
    public Guid MachineId { get; set; }
    public PowerUp? PowerUp { get; set; }
}