using ProductionCalculator.Core.components.entities;

namespace ProductionCalculator.Api.Controllers.dto;

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