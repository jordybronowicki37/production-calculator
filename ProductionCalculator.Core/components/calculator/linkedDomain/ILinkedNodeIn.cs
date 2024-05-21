using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.calculator.linkedDomain;

public interface ILinkedNodeIn: INodeIn
{
    ICollection<LinkedConnection> InConnections { get; }
}