using productionCalculatorLib.components.entities;

namespace SiteReact.Controllers.dto.throughputs;

public class DtoThroughPut
{
    public string Name { get; set; } = string.Empty;
    public Guid Product { get; set; }
    public float Amount { get; set; }

    public DtoThroughPut() {}

    public DtoThroughPut(ThroughPut throughPut)
    {
        Amount = throughPut.Amount;
        Product = throughPut.ProductId;
    }

    public DtoThroughPut(Product product, float amount)
    {
        Amount = amount;
        Product = product.Id;
        Name = product.Name;
    }
}