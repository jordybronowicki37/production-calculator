using productionCalculatorLib.components.entities;

namespace SiteReact.Controllers.dto.products;

public class DtoProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    
    public DtoProduct(Product product)
    {
        Id = product.Id;
        Name = product.Name;
    }
}