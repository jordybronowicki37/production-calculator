using productionCalculatorLib.components.products;

namespace SiteReact.Controllers.dto.products;

public class DtoProduct
{
    public string Name { get; set; } = "";

    public DtoProduct(Product product)
    {
        Name = product.Name;
    }
}