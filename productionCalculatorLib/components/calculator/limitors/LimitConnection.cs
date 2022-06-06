namespace productionCalculatorLib.components.calculator.limitors;

public class LimitConnection
{
    public LimitConnectionTypes Type { get; }
    public float Amount { get; }

    public LimitConnection(LimitConnectionTypes type, float amount)
    {
        Type = type;
        Amount = amount;
    }
}