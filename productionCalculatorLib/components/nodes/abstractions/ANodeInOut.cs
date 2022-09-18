using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.abstractions;

public abstract class ANodeInOut: ANode, INodeInOut
{
    public abstract IList<Connection> InputConnections { get; }
    public abstract void AddInputConnection(Connection connection);
    public abstract IList<Connection> OutputConnections { get; }
    public abstract void AddOutputConnection(Connection connection);
}