namespace productionCalculatorLib.components.worksheet;

public enum WorksheetAlertType
{
    [WorksheetAlertType("The calculation resulted in an overflow, check your nodes for loops", WorksheetAlertLevel.Error)]
    CalculationOverflow,
    
    [WorksheetAlertType("The node is missing an input", WorksheetAlertLevel.Warning)]
    NodeMissingInput,
    
    [WorksheetAlertType("The node is missing an output", WorksheetAlertLevel.Warning)]
    NodeMissingOutput,
    
    [WorksheetAlertType("The connection product does not match the node input", WorksheetAlertLevel.Warning)]
    ConnectionInvalidInput,
    
    [WorksheetAlertType("The connection product does not match the node output", WorksheetAlertLevel.Warning)]
    ConnectionInvalidOutput,
    
    [WorksheetAlertType("The worksheet is empty, try adding some nodes", WorksheetAlertLevel.Information)]
    WorksheetEmpty,
    
    [WorksheetAlertType("The worksheet does not have any targets configured, try adding a target to one of the nodes", WorksheetAlertLevel.Error)]
    WorksheetTargetMissing,
}

[AttributeUsage(AttributeTargets.Field)]
public class WorksheetAlertTypeAttribute : Attribute
{
    public string Message { get; }
    public WorksheetAlertLevel Level { get; }

    public WorksheetAlertTypeAttribute(string message, WorksheetAlertLevel level)
    {
        Message = message;
        Level = level;
    }
}