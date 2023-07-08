namespace productionCalculatorLib.components.targets.exceptions;

public class LimitRuleError: SystemException
{
    public LimitRuleError()
    {
    }

    public LimitRuleError(string? message) : base(message)
    {
    }
}