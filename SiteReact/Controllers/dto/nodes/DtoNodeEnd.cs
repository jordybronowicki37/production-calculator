using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeEnd : NodeDto
{
    public DtoNodeEnd(EndNode node)
    {
        Id = node.Id;
        Type = "End";
        
        Amount = node.Amount;
        Product = node.Product;
        Targets = node.ProductionTargets.Select(t => new DtoProductionTarget(t));
        
        InputNodes = node.InputConnections.Select(n => new DtoConnectionSingle(n.NodeIn.Id, n));
    }
}