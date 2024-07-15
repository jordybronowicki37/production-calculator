using ProductionCalculator.Core.components.worksheet;

namespace ProductionCalculator.Api.Controllers.dto;

public class WorksheetDto
{
    public Guid Id { get; }
    public string Name { get; }
    
    public IEnumerable<AlertDto> Alerts { get; }
    public IEnumerable<NodeDto> Nodes { get; }
    public IEnumerable<ConnectionDto> Connections { get; }

    public WorksheetDto(Worksheet worksheet)
    {
        Id = worksheet.Id;
        Name = worksheet.Name;
        Alerts = worksheet.Alerts.Select(a => new AlertDto(a));
        Nodes = worksheet.Nodes.Select(NodeDto.GenerateNode);
        Connections = worksheet.Connections.Select(c => new ConnectionDto(c));
    }
}