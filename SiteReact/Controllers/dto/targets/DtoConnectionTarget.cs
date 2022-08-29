using productionCalculatorLib.components.calculator.targets;

namespace SiteReact.Controllers.dto.targets;

public class DtoConnectionTarget
{
    public float Amount { get; }
    public string Type { get; }
    
    public DtoConnectionTarget(TargetConnection target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}