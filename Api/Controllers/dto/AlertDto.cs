using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Api.Controllers.dto;

public class AlertDto
{
    public Guid Id { get; }
    public string Name { get; }
    public string Message { get; }
    public string Level { get; }
    public Guid? NodeId { get; }
    public Guid? ConnectionId { get; }
    public Guid? ProductId { get; }
    
    public AlertDto(WorksheetAlert alert)
    {
        Id = alert.Id;
        Name = Enum.GetName(alert.AlertType) ?? string.Empty;
        Message = alert.AlertType.GetMessage();
        Level = Enum.GetName(alert.AlertType.GetLevel()) ?? string.Empty;
        NodeId = alert.NodeId;
        ConnectionId = alert.ConnectionId;
        ProductId = alert.ProductId;
    }
}