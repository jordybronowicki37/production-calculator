using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeProduction : DtoNode
{
    public DtoNodeProduction(ProductionNode node)
    {
        Id = node.Id;
        Type = "Production";
        
        Amount = node.Amount;
        Recipe = node.RecipeId;
        Targets = node.Targets.Select(t => new DtoProductionTarget(t));
    }
}