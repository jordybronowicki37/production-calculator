using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    long Id { get; }
    void RemoveConnnection(long connectionId);
    void ClearConnections();
    IEnumerable<TargetProduction> ProductionTargets { get; }
    void SetExactTarget(float amount);
    void SetMinMaxTarget(float? minAmount, float? maxAmount);
    void ClearTargets();
}