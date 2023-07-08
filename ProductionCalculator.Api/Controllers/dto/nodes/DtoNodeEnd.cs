using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeEnd : DtoNode
{
    public DtoNodeEnd(EndNode node)
    {
        Id = node.Id;
        Type = "End";
        Position = node.Position;
        
        Amount = node.Amount;
        Product = node.ProductId;
        Targets = node.Targets.Select(t => new DtoProductionTarget(t));
    }
}