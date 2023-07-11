using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;

namespace SiteReact.Controllers.dto;

public abstract class NodeDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = "";
    public NodePosition Position { get; set; } = new NodePosition();
    
    public float Amount { get; set; }
    public Guid? Machine { get; set; }
    public Guid? Recipe { get; set; }
    public Guid? Product { get; set; }

    public IEnumerable<ProductionTargetDto> Targets { get; set; } = new List<ProductionTargetDto>();

    public static NodeDto GenerateNode(INode node)
    {
        return node switch
        {
            SpawnNode n => new NodeSpawnDto(n),
            ProductionNode n => new NodeProductionDto(n),
            EndNode n => new NodeEndDto(n),
            _ => throw new InvalidOperationException()
        };
    }
}