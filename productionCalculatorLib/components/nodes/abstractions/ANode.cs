using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.abstractions;

public abstract class ANode: INode
{
    public long Id { get; set; }
    public abstract void RemoveConnnection(long connectionId);
    public abstract void ClearConnections();
    public abstract IList<TargetProduction> ProductionTargets { get; set; }
    public abstract void SetExactTarget(float amount);
    public abstract void SetMinMaxTarget(float? minAmount, float? maxAmount);
    public abstract void ClearTargets();
}