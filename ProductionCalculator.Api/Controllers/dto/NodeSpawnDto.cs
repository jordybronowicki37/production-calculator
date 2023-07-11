using productionCalculatorLib.components.nodes.nodeTypes;

namespace SiteReact.Controllers.dto;

public class NodeSpawnDto : NodeDto
{
    public NodeSpawnDto(SpawnNode node)
    {
        Id = node.Id;
        Type = "Spawn";
        Position = node.Position;
        
        Amount = node.Amount;
        Product = node.ProductId;
        Targets = node.Targets.Select(t => new ProductionTargetDto(t));
    }
}