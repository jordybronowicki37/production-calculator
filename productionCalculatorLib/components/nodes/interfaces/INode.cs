using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    long Id { get; set; }
    ICollection<TargetProduction> Targets { get; set; }
    void SetExactTarget(float amount);
    void SetMinMaxTarget(float? minAmount, float? maxAmount);
    void ClearTargets();
}