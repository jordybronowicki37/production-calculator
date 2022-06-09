using productionCalculatorLib.components.connections;

namespace SiteReact.Controllers.dto.nodes;

public class DtoConnectionDouble
{
    public int InputNodeId { get; }
    public int OutputNodeId { get; }
    public string Product { get; }
    public float Amount { get; }

    public DtoConnectionDouble(Connection connection)
    {
        InputNodeId = connection.NodeIn.Id;
        OutputNodeId = connection.NodeOut.Id;
        Product = connection.Product.Name;
        Amount = connection.Amount;
    }

    protected bool Equals(DtoConnectionDouble other)
    {
        return InputNodeId == other.InputNodeId && OutputNodeId == other.OutputNodeId;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((DtoConnectionDouble) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(InputNodeId, OutputNodeId);
    }
}