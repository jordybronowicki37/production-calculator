using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: ANode, INodeInOut, IHasRecipe
{
    public Guid RecipeId { get; set; }
    public Guid MachineId { get; set; }
    public PowerUp? PowerUp { get; set; }
    
    public ProductionNode() {}
}