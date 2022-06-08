using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.nodeTypes;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeEnd : NodeDto
{
    public DtoNodeEnd(EndNode node)
    {
        Id = node.Id;
        Type = NodeTypes.End.ToString();
        
        Amount = node.Amount;
        Product = node.Product;
        
        InputNodes = node.InputConnections.Select(n => new DtoConnection(n.NodeIn.Id, n));
    }
}