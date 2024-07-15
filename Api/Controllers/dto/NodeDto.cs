using ProductionCalculator.Core.components.nodes;
using ProductionCalculator.Core.components.nodes.interfaces;
using ProductionCalculator.Core.components.nodes.nodeTypes;

namespace ProductionCalculator.Api.Controllers.dto;

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