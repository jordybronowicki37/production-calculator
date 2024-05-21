using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.calculator.linkedDomain;

public sealed class LinkedConnection: Connection
{
    public INodeOut NodeIn { get; }
    public INodeIn NodeOut { get; }
    public Product Product { get; }
    
    public LinkedConnection(EntityContainer entityContainer, Connection connection, INodeOut nodeIn, INodeIn nodeOut) 
        : base(connection.NodeInId, connection.NodeOutId, connection.ProductId)
    {
        Id = connection.Id;
        NodeIn = nodeIn;
        NodeOut = nodeOut;
        Amount = connection.Amount;
        Targets = connection.Targets;
        Product = entityContainer.GetProduct(ProductId)!;
    }
}