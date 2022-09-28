using productionCalculatorLib.components.connections;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.connections;

public class DtoConnectionDouble
{
    public long Id { get; }
    public long InputNodeId { get; }
    public long OutputNodeId { get; }
    public string Product { get; }
    public float Amount { get; }
    public IEnumerable<DtoConnectionTarget> Targets;

    public DtoConnectionDouble(Connection connection)
    {
        Id = connection.Id;
        InputNodeId = connection.NodeInId;
        OutputNodeId = connection.NodeOutId;
        Product = connection.Product.Name;
        Amount = connection.Amount;
        Targets = connection.ConnectionTargets.Select(t => new DtoConnectionTarget(t));
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