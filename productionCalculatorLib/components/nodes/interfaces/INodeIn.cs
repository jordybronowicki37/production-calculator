using productionCalculatorLib.components.connections;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INodeIn: INode
{
    IList<Connection> InputConnections { get; }
    void AddInputConnection(Connection connection);
}