using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: ANode, INodeOut, IHasProduct
{
    public Guid ProductId { get; set; }
    
    public SpawnNode() {}
}