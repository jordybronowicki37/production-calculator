using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    long Id { get; set; }
    void RemoveConnnection(long connectionId);
    void ClearConnections();
    IList<TargetProduction> ProductionTargets { get; set; }
    void SetExactTarget(float amount);
    void SetMinMaxTarget(float? minAmount, float? maxAmount);
    void ClearTargets();
}