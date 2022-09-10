using productionCalculatorLib.components.products;
using SiteReact.Controllers.dto.products;

namespace SiteReact.Controllers.dto.throughputs;

public class DtoThroughPut
{
    public DtoProduct Product { get; set; }
    public float Amount { get; set; }
    
    public DtoThroughPut() {}

    public DtoThroughPut(Product product, float amount)
    {
        Amount = amount;
        Product = new DtoProduct(product);
    }
}