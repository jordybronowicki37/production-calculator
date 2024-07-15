using ProductionCalculator.Core.components.connections;
using ProductionCalculator.Core.components.entities;
using ProductionCalculator.Core.components.entityContainer;
using ProductionCalculator.Core.components.nodes.interfaces;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

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