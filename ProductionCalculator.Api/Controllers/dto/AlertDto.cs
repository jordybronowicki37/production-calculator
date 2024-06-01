using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto;

public class AlertDto
{
    public Guid Id { get; }
    public string Message { get; }
    public WorksheetAlertLevel Level { get; }
    public Guid? NodeId { get; }
    public Guid? ConnectionId { get; }
    public Guid? ProductId { get; }
    
    public AlertDto(WorksheetAlert alert)
    {
        Id = alert.Id;
        Message = alert.Message;
        Level = alert.Level;
        NodeId = alert.NodeId;
        ConnectionId = alert.ConnectionId;
        ProductId = alert.ProductId;
    }
}