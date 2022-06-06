using productionCalculatorLib.components.connections;
using SiteReact.Controllers.dto.products;

namespace SiteReact.Controllers.dto.nodes;

public class DtoConnection
{
    public int OtherNodeId { get; }
    public string Product { get; }
    public float Amount { get; }

    public DtoConnection(int otherNodeId, Connection connection)
    {
        OtherNodeId = otherNodeId;
        Product = connection.Product.Name;
        Amount = connection.Amount;
    }
}