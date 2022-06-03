using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.nodes;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheet
{
    public string Name { get; }
    public IEnumerable<NodeDto> Nodes { get; }

    public DtoWorksheet(Worksheet worksheet)
    {
        Name = worksheet.Name;
        Nodes = worksheet.Nodes.Select(n => _generateNodeDTO(worksheet, n));
    }

    private static NodeDto _generateNodeDTO(Worksheet worksheet, INode node)
    {
        return node switch
        {
            SpawnNode n => new DtoNodeSpawn(worksheet, n),
            ProductionNode n => new DtoNodeProduction(worksheet, n),
            EndNode n => new DtoNodeEnd(worksheet, n),
            _ => throw new InvalidOperationException()
        };
    }
}