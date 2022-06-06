using productionCalculatorLib.components.connections;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface INode
{
    int Id { get; }
    void RemoveConnnection(Connection connection);
}