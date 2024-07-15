namespace ProductionCalculator.Api.Controllers.dto;

public class ConnectionCreateDto
{
    public Guid InputNodeId { get; set; }
    public Guid OutputNodeId { get; set; }
    public Guid Product { get; set; }
}