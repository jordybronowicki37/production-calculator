using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using SiteReact.Controllers.dto.targets;

namespace SiteReact.Controllers.dto.nodes;

public class DtoNode
{
    public Guid Id { get; set; }
    public string Type { get; set; } = "";
    
    public float Amount { get; set; }
    public Guid? Machine { get; set; }
    public Guid? Recipe { get; set; }
    public Guid? Product { get; set; }
    
    public IEnumerable<DtoProductionTarget>? Targets { get; set; }

    public static DtoNode GenerateNode(INode node)
    {
        return node switch
        {
            SpawnNode n => new DtoNodeSpawn(n),
            ProductionNode n => new DtoNodeProduction(n),
            EndNode n => new DtoNodeEnd(n),
            _ => throw new InvalidOperationException()
        };
    }
}