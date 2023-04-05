namespace SiteReact.Controllers.dto.nodes;

public class DtoNodeCreate
{
    public string Type { get; set; } = "";
    public Guid Product { get; set; }
    public Guid Recipe { get; set; }
    public Guid Machine { get; set; }
}