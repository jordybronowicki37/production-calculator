using ProductionCalculator.Core.components.nodes;

namespace ProductionCalculator.Api.Controllers.dto;

public class NodeCreateDto
{
    public string Type { get; set; } = "";
    public NodePosition Position { get; set; } = new NodePosition();
    public Guid Product { get; set; }
    public Guid Recipe { get; set; }
    public Guid Machine { get; set; }
}