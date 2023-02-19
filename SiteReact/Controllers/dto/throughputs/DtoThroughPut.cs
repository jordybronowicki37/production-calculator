using productionCalculatorLib.components.entities;

namespace SiteReact.Controllers.dto.throughputs;

public class DtoThroughPut
{
    public Guid Product { get; set; }
    public float Amount { get; set; }

    public DtoThroughPut() {}

    public DtoThroughPut(ThroughPut throughPut)
    {
        Amount = throughPut.Amount;
        Product = throughPut.ProductId;
    }

    public DtoThroughPut(Guid product, float amount)
    {
        Amount = amount;
        Product = product;
    }
}