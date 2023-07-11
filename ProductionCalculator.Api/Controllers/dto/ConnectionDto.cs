using productionCalculatorLib.components.connections;

namespace SiteReact.Controllers.dto;

public class ConnectionDto
{
    public Guid Id { get; }
    public Guid InputNodeId { get; }
    public Guid OutputNodeId { get; }
    public Guid ProductId { get; }
    public float Amount { get; }
    public IEnumerable<ConnectionTargetDto> Targets;

    public ConnectionDto(Connection connection)
    {
        Id = connection.Id;
        InputNodeId = connection.NodeInId;
        OutputNodeId = connection.NodeOutId;
        ProductId = connection.ProductId;
        Amount = connection.Amount;
        Targets = connection.Targets.Select(t => new ConnectionTargetDto(t));
    }

    protected bool Equals(ConnectionDto other)
    {
        return InputNodeId == other.InputNodeId && OutputNodeId == other.OutputNodeId;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((ConnectionDto) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(InputNodeId, OutputNodeId);
    }
}