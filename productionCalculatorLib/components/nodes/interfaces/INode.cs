using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    long Id { get; }
    void RemoveConnnection(long connectionId);
    List<TargetProduction> ProductionTargets { get; }
    void AddProductionTarget(TargetProduction target);
    void RemoveProductionTarget(TargetProduction target);
}