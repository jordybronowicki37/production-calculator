using productionCalculatorLib.components.products;

namespace SiteReact.Controllers.dto.nodes;

public class NodeDto
{
    public long Id { get; set; }
    public string Type { get; set; }
    
    public float Amount { get; set; }
    public Recipe? Recipe { get; set; }
    public Product? Product { get; set; }
    
    public IEnumerable<DtoConnectionSingle>? InputNodes { get; set; }
    public IEnumerable<DtoConnectionSingle>? OutputNodes { get; set; }
}