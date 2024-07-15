using ProductionCalculator.Core.components.nodes.interfaces;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

public interface ILinkedNodeOut: INodeOut
{
    ICollection<LinkedConnection> OutConnections { get; }
}