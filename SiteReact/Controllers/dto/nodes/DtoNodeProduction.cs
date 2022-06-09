using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.nodeTypes;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeProduction : NodeDto
{
    public DtoNodeProduction(ProductionNode node)
    {
        Id = node.Id;
        Type = NodeTypes.Production.ToString();;
        
        Amount = node.ProductionAmount;
        Recipe = node.Recipe;
        
        InputNodes = node.InputConnections.Select(n => new DtoConnectionSingle(n.NodeIn.Id, n));
        OutputNodes = node.OutputConnections.Select(n => new DtoConnectionSingle(n.NodeOut.Id, n));
    }
}