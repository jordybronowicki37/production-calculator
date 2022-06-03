using productionCalculatorLib.components.products;

namespace SiteReact.Controllers.dto.nodes;

public class NodeDto
{
    public int Id { get; set; }
    public string Type { get; set; }
    
    public int Amount { get; set; }
    public Recipe? Recipe { get; set; }
    public Product? Product { get; set; }
    
    public IEnumerable<int>? InputNodes { get; set; }
    public IEnumerable<int>? OutputNodes { get; set; }
}