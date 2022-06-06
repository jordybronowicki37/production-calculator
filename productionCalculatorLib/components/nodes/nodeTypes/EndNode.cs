using System.Collections.ObjectModel;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: INodeIn, IHasProduct
{
    public int Id { get; }
    private readonly List<Connection> _inputConnections = new();
    public Product Product { get; set; }
    public float Amount { get; set; }

    public EndNode(int id, Product product)
    {
        Id = id;
        Product = product;
    }

    public IList<Connection> InputConnections => new ReadOnlyCollection<Connection>(_inputConnections);
    
    public void AddInputConnection(Connection connection)
    {
        _inputConnections.Add(connection);
        // if (!node.OutputNodes.Contains(this)) node.AddOutputConnection(this);
    }
    
    public void RemoveConnnection(Connection connection)
    {
        // _inputConnections.Remove(node);
    }
}