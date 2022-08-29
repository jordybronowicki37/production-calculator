using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeSpawn : NodeDto
{
    public DtoNodeSpawn(SpawnNode node)
    {
        Id = node.Id;
        Type = "Spawn";
        
        Amount = node.Amount;
        Product = node.Product;
        Targets = node.ProductionTargets.Select(t => new DtoProductionTarget(t));
        
        OutputNodes = node.OutputConnections.Select(n => new DtoConnectionSingle(n.NodeOut.Id, n));
    }
}