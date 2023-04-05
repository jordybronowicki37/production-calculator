using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: ANode, INodeIn, IHasProduct
{
    public Guid ProductId { get; set; }
    
    public EndNode() {}
}