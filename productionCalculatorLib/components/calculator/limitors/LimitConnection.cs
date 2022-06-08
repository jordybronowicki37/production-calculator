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

    protected bool Equals(LimitConnection other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((LimitConnection) obj);
    }

    public override int GetHashCode()
    {
        return (int) Type;
    }

    public static bool operator ==(LimitConnection? left, LimitConnection? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(LimitConnection? left, LimitConnection? right)
    {
        return !Equals(left, right);
    }
}