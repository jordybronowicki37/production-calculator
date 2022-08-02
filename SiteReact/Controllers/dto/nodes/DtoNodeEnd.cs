using productionCalculatorLib.components.nodes.nodeTypes;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeEnd : NodeDto
{
    public DtoNodeEnd(EndNode node)
    {
        Id = node.Id;
        Type = "End";
        
        Amount = node.Amount;
        Product = node.Product;
        
        InputNodes = node.InputConnections.Select(n => new DtoConnectionSingle(n.NodeIn.Id, n));
    }
}