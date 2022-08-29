using productionCalculatorLib.components.connections;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.connections;

public class DtoConnectionSingle
{
    public long Id { get; }
    public long OtherNodeId { get; }
    public string Product { get; }
    public float Amount { get; }
    public IEnumerable<DtoConnectionTarget> Targets;

    public DtoConnectionSingle(long otherNodeId, Connection connection)
    {
        Id = connection.Id;
        OtherNodeId = otherNodeId;
        Product = connection.Product.Name;
        Amount = connection.Amount;
        Targets = connection.ConnectionTargets.Select(t => new DtoConnectionTarget(t));
    }
}