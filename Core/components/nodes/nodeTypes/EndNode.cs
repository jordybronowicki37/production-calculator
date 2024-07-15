using ProductionCalculator.Core.components.nodes.abstractions;
using ProductionCalculator.Core.components.nodes.interfaces;

namespace ProductionCalculator.Core.components.nodes.nodeTypes;

public class EndNode: ANode, INodeIn, IHasProduct
{
    public Guid ProductId { get; set; }
    
    public EndNode() {}
}