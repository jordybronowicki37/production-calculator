namespace SiteReact.Controllers.dto.connections;

public class DtoConnectionCreate
{
    public long InputNodeId { get; set; }
    public long OutputNodeId { get; set; }
    public string Product { get; set; } = "";
}