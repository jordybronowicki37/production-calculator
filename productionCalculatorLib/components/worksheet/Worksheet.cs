using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public Worksheet() {}

    public long Id { get; set; }
    public string Name { get; set; } = "";
    public virtual EntityContainer EntityContainer { get; } = new();

    public bool CalculationSucceeded { get; set; } = true;
    public string CalculationError { get; set; } = "";

    public virtual IList<ANode> Nodes { get; } = new List<ANode>();
    public void AddNode(ANode node)
    {
        if (!Nodes.Contains(node))
        {
            Nodes.Add(node);
        }
    }
    public void RemoveNode(ANode node)
    {
        Nodes.Remove(node);
        ClearNodeConnections(node);
    }
    public NodeBuilder<TNodeType> GetNodeBuilder<TNodeType>() where TNodeType : ANode, new()
    {
        return new NodeBuilder<TNodeType>(this);
    }

    public virtual IList<Connection> Connections { get; private set; } = new List<Connection>();
    public void AddConnection(Connection connection)
    {
        if (!Connections.Contains(connection))
        {
            Connections.Add(connection);
        }
    }
    public void RemoveConnection(Connection connection)
    {
        Connections.Remove(connection);
    }
    public void ClearNodeConnections(ANode node)
    {
        var id = node.Id;
        var filtered = Connections.Where(c => c.NodeInId != id && c.NodeOutId != id);
        Connections = new List<Connection>(filtered);
    }
    public ConnectionBuilder GetConnectionBuilder(INodeOut nodeOut, INodeIn nodeIn, Product product)
    {
        return new ConnectionBuilder(this, nodeOut, nodeIn, product);
    }
}