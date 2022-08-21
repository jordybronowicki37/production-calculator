using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.nodes;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheet
{
    public long Id { get; }
    public string Name { get; }
    
    public bool CalculationSucceeded { get; }
    public string CalculationError { get; }
    
    public IEnumerable<NodeDto> Nodes { get; }
    public List<DtoConnectionDouble> Connections { get; } = new();

    public DtoWorksheet(Worksheet worksheet)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        CalculationSucceeded = worksheet.CalculationSucceeded;
        CalculationError = worksheet.CalculationError;
        Nodes = worksheet.Nodes.Select(n => NodeDto.GenerateNode(n));
        
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
}