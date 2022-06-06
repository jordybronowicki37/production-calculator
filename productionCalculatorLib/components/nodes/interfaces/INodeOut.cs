using productionCalculatorLib.components.connections;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INodeOut: INode
{
    IList<Connection> OutputConnections { get; }
    void AddOutputConnection(Connection connection);
}