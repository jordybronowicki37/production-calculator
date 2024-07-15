using ProductionCalculator.Core.components.nodes.abstractions;
using ProductionCalculator.Core.components.nodes.interfaces;

namespace ProductionCalculator.Core.components.nodes.nodeTypes;

public class SpawnNode: ANode, INodeOut, IHasProduct
{
    public Guid ProductId { get; set; }
}