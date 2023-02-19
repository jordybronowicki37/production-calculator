using productionCalculatorLib.components.worksheet;
using SiteReact.Controllers.dto.connections;
using SiteReact.Controllers.dto.nodes;

namespace SiteReact.Controllers.dto.worksheets;

public class DtoWorksheet
{
    public Guid Id { get; }
    public string Name { get; }
    
    public bool CalculationSucceeded { get; }
    public string CalculationError { get; }
    
    public IEnumerable<DtoNode> Nodes { get; }
    public IEnumerable<DtoConnectionDouble> Connections { get; }

    public DtoWorksheet(Worksheet worksheet)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        CalculationSucceeded = worksheet.CalculationSucceeded;
        CalculationError = worksheet.CalculationError;
        Nodes = worksheet.Nodes.Select(DtoNode.GenerateNode);
        Connections = worksheet.Connections.Select(c => new DtoConnectionDouble(c));
    }
}