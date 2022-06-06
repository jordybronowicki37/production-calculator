namespace productionCalculatorLib.components.calculator.limitors;

public class LimitProduction
{
    public LimitProductionTypes Type { get; }
    public float Amount { get; }

    public LimitProduction(LimitProductionTypes type, float amount)
    {
        Type = type;
        Amount = amount;
    }
}