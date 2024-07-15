using ProductionCalculator.Core.components.entities;
using ProductionCalculator.Core.components.entityContainer;
using ProductionCalculator.Core.components.nodes.nodeTypes;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

public sealed class LinkedSpawnNode: SpawnNode, ILinkedHasProduct, ILinkedNodeOut
{
    public Product Product { get; }
    public ICollection<LinkedConnection> OutConnections { get; } = new List<LinkedConnection>();

    public LinkedSpawnNode(EntityContainer entityContainer, SpawnNode spawnNode)
    {
        Id = spawnNode.Id;
        Amount = spawnNode.Amount;
        Position = spawnNode.Position;
        Targets = spawnNode.Targets;
        ProductId = spawnNode.ProductId;
        Product = entityContainer.GetProduct(ProductId)!;
    }
}