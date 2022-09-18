using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.abstractions;

public abstract class ANodeOut : ANode, INodeOut
{
    public virtual IList<Connection> OutputConnections { get; }
    public abstract void AddOutputConnection(Connection connection);
}