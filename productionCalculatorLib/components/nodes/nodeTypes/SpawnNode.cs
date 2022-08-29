using System.Collections.ObjectModel;
using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: INodeOut, IHasProduct
{
    public long Id { get; } = IdGenerators.NodeId;
    private readonly List<Connection> _outputConnections = new();
    public Product Product { get; set; } = null!;
    public float Amount { get; set; }
    
    public SpawnNode() {}

    public SpawnNode(Product product)
    {
        Product = product;
    }

    public IList<Connection> OutputConnections => new ReadOnlyCollection<Connection>(_outputConnections);
    public void AddOutputConnection(Connection connection)
    {
        if (!_outputConnections.Contains(connection))_outputConnections.Add(connection);
    }
    public void RemoveConnnection(long connectionId)
    {
        var connection = _outputConnections.Find(c => c.Id == connectionId);
        if (connection == null) return;
        _outputConnections.Remove(connection);
    }
    
    public List<TargetProduction> ProductionTargets { get; } = new();
    public void AddProductionTarget(TargetProduction target)
    {
        if (!ProductionTargets.Contains(target)) ProductionTargets.Add(target);
    }
    public void RemoveProductionTarget(TargetProduction target)
    {
        ProductionTargets.Remove(target);
    }
}