namespace ProductionCalculator.Core.components.targets;

public class TargetConnection
{
    public TargetConnectionTypes Type { get; set; }
    public float Amount { get; set; }
    
    public TargetConnection() {}

    public TargetConnection(TargetConnectionTypes type, float amount)
    {
        Type = type;
        Amount = amount;
    }

    protected bool Equals(TargetConnection other)
    {
        return Type == other.Type;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((TargetConnection) obj);
    }

    public override int GetHashCode()
    {
        return (int) Type;
    }

    public static bool operator ==(TargetConnection? left, TargetConnection? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(TargetConnection? left, TargetConnection? right)
    {
        return !Equals(left, right);
    }
}