using productionCalculatorLib.components.targets;

namespace SiteReact.Controllers.dto;

public class ConnectionTargetDto
{
    public float Amount { get; set; }
    public string Type { get; set; } = string.Empty;
    
    public ConnectionTargetDto() {}
    
    public ConnectionTargetDto(TargetConnection target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}