using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.nodeTypes;

namespace productionCalculatorLib.components.calculator.linkedDomain;

public sealed class LinkedEndNode: EndNode, ILinkedHasProduct, ILinkedNodeIn 
{
    public Product Product { get; }
    public ICollection<LinkedConnection> InConnections { get; } = new List<LinkedConnection>();

    public LinkedEndNode(EntityContainer entityContainer, EndNode endNode)
    {
        Id = endNode.Id;
        Amount = endNode.Amount;
        Position = endNode.Position;
        Targets = endNode.Targets;
        ProductId = endNode.ProductId;
        Product = entityContainer.GetProduct(ProductId)!;
    }
}