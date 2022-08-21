using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;

namespace SiteReact.Controllers.dto.nodes;

public class NodeDto
{
    public long Id { get; set; }
    public string Type { get; set; } = "";
    
    public float Amount { get; set; }
    public Recipe? Recipe { get; set; }
    public Product? Product { get; set; }
    
    public IEnumerable<DtoConnectionSingle>? InputNodes { get; set; }
    public IEnumerable<DtoConnectionSingle>? OutputNodes { get; set; }

    public static NodeDto GenerateNode(INode node)
    {
        return node switch
        {
            SpawnNode n => new DtoNodeSpawn(n),
            ProductionNode n => new DtoNodeProduction(n),
            EndNode n => new DtoNodeEnd(n),
            _ => throw new InvalidOperationException()
        };
    }
}