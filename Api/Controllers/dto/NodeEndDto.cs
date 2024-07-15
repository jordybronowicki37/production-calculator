using ProductionCalculator.Core.components.nodes.nodeTypes;

namespace ProductionCalculator.Api.Controllers.dto;

public class NodeEndDto : NodeDto
{
    public NodeEndDto(EndNode node)
    {
        Id = node.Id;
        Type = "End";
        Position = node.Position;
        
        Amount = node.Amount;
        Product = node.ProductId;
        Targets = node.Targets.Select(t => new ProductionTargetDto(t));
    }
}