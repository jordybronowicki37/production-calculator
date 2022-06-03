using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeEnd : NodeDto
{
    public DtoNodeEnd(Worksheet worksheet, EndNode node)
    {
        var nodes = worksheet.Nodes;
        
        Id = nodes.IndexOf(node);
        Type = "End";
        
        Amount = node.Amount;
        Product = node.Product;
        
        InputNodes = node.InputNodes.Select(n => nodes.IndexOf(n));
    }
}