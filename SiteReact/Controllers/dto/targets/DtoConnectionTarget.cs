using productionCalculatorLib.components.calculator.targets;

namespace SiteReact.Controllers.dto.targets;

public class DtoConnectionTarget
{
    public float Amount;
    public TargetConnectionTypes Type;
    
    public DtoConnectionTarget(TargetConnection target)
    {
        Amount = target.Amount;
        Type = target.Type;
    }
}