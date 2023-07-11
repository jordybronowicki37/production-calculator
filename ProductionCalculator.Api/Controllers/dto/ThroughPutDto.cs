using productionCalculatorLib.components.entities;

namespace SiteReact.Controllers.dto;

public class ThroughPutDto
{
    public string Name { get; set; } = string.Empty;
    public Guid Product { get; set; }
    public float Amount { get; set; }

    public ThroughPutDto() {}

    public ThroughPutDto(ThroughPut throughPut)
    {
        Amount = throughPut.Amount;
        Product = throughPut.ProductId;
    }

    public ThroughPutDto(Product product, float amount)
    {
        Amount = amount;
        Product = product.Id;
        Name = product.Name;
    }
}