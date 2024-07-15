using ProductionCalculator.Core.components.nodes.nodeTypes;

namespace ProductionCalculator.Api.Controllers.dto;

public class NodeProductionDto : NodeDto
{
    public NodeProductionDto(ProductionNode node)
    {
        Id = node.Id;
        Type = "Production";
        Position = node.Position;
        
        Amount = node.Amount;
        Machine = node.MachineId;
        Recipe = node.RecipeId;
        Targets = node.Targets.Select(t => new ProductionTargetDto(t));
    }
}