using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.nodes;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheet
{
    public long Id { get; }
    public string Name { get; }
    public IEnumerable<NodeDto> Nodes { get; }
    public List<DtoConnectionDouble> Connections { get; } = new List<DtoConnectionDouble>();

    public DtoWorksheet(Worksheet worksheet)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        Nodes = worksheet.Nodes.Select(n => _generateNodeDTO(worksheet, n));
        
        foreach (var node in worksheet.Nodes)
        {
            if (node is INodeIn nodeIn)
            {
                foreach (var inConnection in nodeIn.InputConnections)
                {
                    var dto = new DtoConnectionDouble(inConnection);
                    if (!Connections.Contains(dto)) Connections.Add(dto);
                }
            }
            if (node is INodeOut nodeOut)
            {
                foreach (var outConnection in nodeOut.OutputConnections)
                {
                    var dto = new DtoConnectionDouble(outConnection);
                    if (!Connections.Contains(dto)) Connections.Add(dto);
                }
            }
        }
    }

    private static NodeDto _generateNodeDTO(Worksheet worksheet, INode node)
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