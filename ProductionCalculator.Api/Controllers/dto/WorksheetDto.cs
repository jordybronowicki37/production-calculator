using productionCalculatorLib.components.worksheet;

namespace SiteReact.Controllers.dto;

public class WorksheetDto
{
    public Guid Id { get; }
    public string Name { get; }
    
    public bool CalculationSucceeded { get; }
    public IEnumerable<WorksheetAlert> Alerts { get; }
    
    public IEnumerable<NodeDto> Nodes { get; }
    public IEnumerable<ConnectionDto> Connections { get; }

    public WorksheetDto(Worksheet worksheet)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        CalculationSucceeded = worksheet.CalculationSucceeded;
        Alerts = worksheet.Alerts;
        Nodes = worksheet.Nodes.Select(NodeDto.GenerateNode);
        Connections = worksheet.Connections.Select(c => new ConnectionDto(c));
    }
}