using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.calculator.linkedDomain;

public interface ILinkedNodeOut: INodeOut
{
    ICollection<LinkedConnection> OutConnections { get; }
}