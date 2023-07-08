using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeSpawn : DtoNode
{
    public DtoNodeSpawn(SpawnNode node)
    {
        Id = node.Id;
        Type = "Spawn";
        Position = node.Position;
        
        Amount = node.Amount;
        Product = node.ProductId;
        Targets = node.Targets.Select(t => new DtoProductionTarget(t));
    }
}