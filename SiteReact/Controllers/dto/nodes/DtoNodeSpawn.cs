using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeSpawn : NodeDto
{
    public DtoNodeSpawn(Worksheet worksheet, SpawnNode node)
    {
        var nodes = worksheet.Nodes;
        
        Id = node.Id;
        Type = "Spawn";
        
        Amount = node.Amount;
        Product = node.Product;
        
        OutputNodes = node.OutputNodes.Select(n => n.Id);
    }
}