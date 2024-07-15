namespace ProductionCalculator.Core.components.worksheet;

public class WorksheetAlert
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public WorksheetAlertType AlertType { get; init; }
    public Guid? NodeId { get; init; }
    public Guid? ConnectionId { get; init; }
    public Guid? ProductId { get; init; }
    
    public WorksheetAlert(WorksheetAlertType alertType)
    {
        AlertType = alertType;
    }
}