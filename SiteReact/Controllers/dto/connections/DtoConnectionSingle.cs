using productionCalculatorLib.components.connections;

namespace SiteReact.Controllers.dto.connections;

public class DtoConnectionSingle
{
    public long Id { get; }
    public long OtherNodeId { get; }
    public string Product { get; }
    public float Amount { get; }

    public DtoConnectionSingle(long otherNodeId, Connection connection)
    {
        Id = connection.Id;
        OtherNodeId = otherNodeId;
        Product = connection.Product.Name;
        Amount = connection.Amount;
    }
}