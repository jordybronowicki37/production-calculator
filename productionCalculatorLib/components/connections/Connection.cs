using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.connections;

public class Connection
{
    public long Id { get; set; }
    public ANode NodeIn { get; }
    public ANode NodeOut { get; }
    public virtual Product Product { get; set; }
    public float Amount { get; set; }
    
    public Connection() {}

    public Connection(INodeOut nodeIn, INodeIn nodeOut, Product product)
    {
        NodeIn = (ANode) nodeIn;
        NodeOut = (ANode) nodeOut;
        Product = product;
    }

    public virtual List<TargetConnection> ConnectionTargets { get; } = new();
    public void AddConnectionTarget(TargetConnection target)
    {
        if (!ConnectionTargets.Contains(target)) ConnectionTargets.Add(target);
    }
    public void RemoveConnectionTarget(TargetConnection target)
    {
        ConnectionTargets.Remove(target);
    }

    protected bool Equals(Connection other)
    {
        return NodeIn.Equals(other.NodeIn) && NodeOut.Equals(other.NodeOut) && Product.Equals(other.Product);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Connection) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(NodeIn, NodeOut);
    }

    public static bool operator ==(Connection? left, Connection? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Connection? left, Connection? right)
    {
        return !Equals(left, right);
    }
}