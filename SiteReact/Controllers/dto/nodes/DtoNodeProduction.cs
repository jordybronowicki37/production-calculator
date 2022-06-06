using productionCalculatorLib.components.nodes.nodeTypes;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeProduction : NodeDto
{
    public DtoNodeProduction(ProductionNode node)
    {
        Id = node.Id;
        Type = "Production";
        
        Amount = node.ProductionAmount;
        Recipe = node.Recipe;
        
        InputNodes = node.InputConnections.Select(n => new DtoConnection(n.NodeIn.Id, n));
        OutputNodes = node.OutputConnections.Select(n => new DtoConnection(n.NodeOut.Id, n));
    }
}