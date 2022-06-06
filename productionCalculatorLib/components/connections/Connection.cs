using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.connections;

public class Connection
{
    public INodeOut NodeIn { get; set; }
    public INodeIn NodeOut { get; set; }
    public Product Product { get; set; }
    public float Amount { get; set; }

    public Connection(INodeOut nodeIn, INodeIn nodeOut, Product product)
    {
        NodeIn = nodeIn;
        NodeOut = nodeOut;
        Product = product;
    }

    protected bool Equals(Connection other)
    {
        return NodeIn.Equals(other.NodeIn) && NodeOut.Equals(other.NodeOut) && Product.Equals(other.Product);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
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