using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.abstractions;

public abstract class ANodeIn: ANode, INodeIn
{
    public abstract IList<Connection> InputConnections { get; }
    public abstract void AddInputConnection(Connection connection);
}