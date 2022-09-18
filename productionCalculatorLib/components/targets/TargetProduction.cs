namespace productionCalculatorLib.components.targets;

public class TargetProduction
{
    public TargetProductionTypes Type { get; }
    public float Amount { get; }
    
    public TargetProduction() {}

    public TargetProduction(TargetProductionTypes type, float amount)
    {
        Type = type;
        Amount = amount;
    }

    protected bool Equals(TargetProduction other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TargetProduction) obj);
    }

    public override int GetHashCode()
    {
        return (int) Type;
    }

    public static bool operator ==(TargetProduction? left, TargetProduction? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TargetProduction? left, TargetProduction? right)
    {
        return !Equals(left, right);
    }
}