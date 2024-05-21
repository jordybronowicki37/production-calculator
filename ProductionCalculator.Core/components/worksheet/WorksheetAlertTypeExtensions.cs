namespace productionCalculatorLib.components.worksheet;

public static class WorksheetAlertTypeExtensions
{
    public static string GetMessage(this WorksheetAlertType value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null) return string.Empty;
        var field = type.GetField(name);
        if (field == null) return string.Empty;
        return Attribute.GetCustomAttribute(field, typeof(WorksheetAlertTypeAttribute)) is WorksheetAlertTypeAttribute attr ? attr.Message : string.Empty;
    }

    public static WorksheetAlertLevel GetLevel(this WorksheetAlertType value)
    {
        var type = value.GetType();
        var name = Enum.GetName(type, value);
        if (name == null) return WorksheetAlertLevel.Unknown;
        var field = type.GetField(name);
        if (field == null) return WorksheetAlertLevel.Unknown;
        return Attribute.GetCustomAttribute(field, typeof(WorksheetAlertTypeAttribute)) is WorksheetAlertTypeAttribute attr ? attr.Level : WorksheetAlertLevel.Unknown;
    }
}