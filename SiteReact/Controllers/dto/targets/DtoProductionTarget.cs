using productionCalculatorLib.components.targets;

namespace SiteReact.Controllers.dto.targets;

public class DtoProductionTarget
{
    public float Amount { get; set; }
    public string Type { get; set; }
    
    public DtoProductionTarget(TargetProduction target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}