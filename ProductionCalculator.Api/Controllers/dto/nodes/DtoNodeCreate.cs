using productionCalculatorLib.components.nodes;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeCreate
{
    public string Type { get; set; } = "";
    public NodePosition Position { get; set; } = new NodePosition();
    public Guid Product { get; set; }
    public Guid Recipe { get; set; }
    public Guid Machine { get; set; }
}