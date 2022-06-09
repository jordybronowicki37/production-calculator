using productionCalculatorLib.components.connections;

namespace SiteReact.Controllers.dto.nodes;

public class DtoConnectionSingle
{
    public int OtherNodeId { get; }
    public string Product { get; }
    public float Amount { get; }

    public DtoConnectionSingle(int otherNodeId, Connection connection)
    {
        OtherNodeId = otherNodeId;
        Product = connection.Product.Name;
        Amount = connection.Amount;
    }
}