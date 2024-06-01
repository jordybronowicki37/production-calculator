namespace productionCalculatorLib.components.worksheet;

public class WorksheetAlert
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Message { get; set; }
    public WorksheetAlertLevel Level { get; set; }
    public Guid? NodeId { get; set; }
    public Guid? ConnectionId { get; set; }
    public Guid? ProductId { get; set; }
    
    public WorksheetAlert(WorksheetAlertType alertType)
    {
        Message = alertType.GetMessage();
        Level = alertType.GetLevel();
    }
}