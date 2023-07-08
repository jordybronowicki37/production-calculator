namespace SiteReact.Controllers.dto.connections;

public class DtoConnectionCreate
{
    public Guid InputNodeId { get; set; }
    public Guid OutputNodeId { get; set; }
    public Guid Product { get; set; }
}