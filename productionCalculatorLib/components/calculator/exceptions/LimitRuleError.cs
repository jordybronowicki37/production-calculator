namespace productionCalculatorLib.components.calculator.exceptions;

public class LimitRuleError: SystemException
{
    public LimitRuleError()
    {
    }

    public LimitRuleError(string? message) : base(message)
    {
    }
}