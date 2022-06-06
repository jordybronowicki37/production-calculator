using System.Collections.ObjectModel;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: INodeOut, IHasProduct
{
    public int Id { get; }
    private readonly List<Connection> _outputConnections = new();
    public Product Product { get; set; }
    public float Amount { get; set; }

    public SpawnNode(int id, Product product)
    {
        Id = id;
        Product = product;
    }

    public IList<Connection> OutputConnections => new ReadOnlyCollection<Connection>(_outputConnections);
    
    public void AddOutputConnection(Connection connection)
    {
        if (!_outputConnections.Contains(connection))_outputConnections.Add(connection);
    }
    
    public void RemoveConnnection(Connection connection)
    {
        _outputConnections.Remove(connection);
    }
}