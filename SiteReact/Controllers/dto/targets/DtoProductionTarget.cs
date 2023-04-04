using productionCalculatorLib.components.targets;

namespace SiteReact.Controllers.dto.targets;

public class DtoProductionTarget
{
    public float Amount { get; set; }
    public string Type { get; set; } = string.Empty;

    public DtoProductionTarget() {}

    public DtoProductionTarget(TargetProduction target)
    {
        Amount = target.Amount;
        Type = target.Type.ToString();
    }
}