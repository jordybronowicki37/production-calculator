using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool CalculationSucceeded { get; set; } = true;
    public ICollection<WorksheetAlert> Alerts { get; set; } = new List<WorksheetAlert>();
    
    public Guid EntityContainerId { get; private set; }
    public ICollection<ANode> Nodes { get; private set; } = new List<ANode>();
    public ICollection<Connection> Connections { get; private set; } = new List<Connection>();
    
    public Worksheet(string name, Guid entityContainerId)
    {
        Name = name;
        EntityContainerId = entityContainerId;
    }
    
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