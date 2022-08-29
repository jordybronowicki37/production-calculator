using productionCalculatorLib.components.calculator.targets;

namespace SiteReact.Controllers.dto.targets;

public class DtoProductionTarget
{
    public float Amount { get; }
    public string Type { get; }
    
    public DtoProductionTarget(TargetProduction target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}