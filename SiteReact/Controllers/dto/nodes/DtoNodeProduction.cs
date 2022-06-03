using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeProduction : NodeDto
{
    public DtoNodeProduction(Worksheet worksheet, ProductionNode node)
    {
        var nodes = worksheet.Nodes;
        
        Id = nodes.IndexOf(node);
        Type = "Production";
        
        Amount = node.ProductionAmount;
        Recipe = node.Recipe;
        
        InputNodes = node.InputNodes.Select(n => nodes.IndexOf(n));
        OutputNodes = node.OutputNodes.Select(n => nodes.IndexOf(n));
    }
}