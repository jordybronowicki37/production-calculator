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

    protected bool Equals(LimitProduction other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LimitProduction) obj);
    }

    public override int GetHashCode()
    {
        return (int) Type;
    }

    public static bool operator ==(LimitProduction? left, LimitProduction? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LimitProduction? left, LimitProduction? right)
    {
        return !Equals(left, right);
    }
}