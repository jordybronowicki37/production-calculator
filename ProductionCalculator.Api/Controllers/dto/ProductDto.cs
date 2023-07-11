using productionCalculatorLib.components.entities;

namespace SiteReact.Controllers.dto;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    
    public ProductDto(Product product)
    {
        Id = product.Id;
        Name = product.Name;
    }
}