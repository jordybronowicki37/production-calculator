using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.abstractions;

public abstract class ANode: INode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public virtual ICollection<TargetProduction> Targets { get; set; } = new List<TargetProduction>();
    public abstract void SetExactTarget(float amount);
    public abstract void SetMinMaxTarget(float? minAmount, float? maxAmount);
    public abstract void ClearTargets();
}