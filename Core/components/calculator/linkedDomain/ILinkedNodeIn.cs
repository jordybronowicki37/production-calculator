using ProductionCalculator.Core.components.nodes.interfaces;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

public interface ILinkedNodeIn: INodeIn
{
    ICollection<LinkedConnection> InConnections { get; }
}