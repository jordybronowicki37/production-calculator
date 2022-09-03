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
    private readonly List<TargetProduction> _targets = new();
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
    
    public IEnumerable<TargetProduction> ProductionTargets => new ReadOnlyCollection<TargetProduction>(_targets);
    public void SetExactTarget(float amount) 
    {
        ClearTargets();
        _targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        Amount = amount;
    }
    public void SetMinMaxTarget(float? minAmount, float? maxAmount) 
    {
        ClearTargets();
        if (minAmount != null)
        {
            _targets.Add(new TargetProduction(TargetProductionTypes.MinAmount, (float) minAmount));
            Amount = (float) minAmount;
        }
        if (maxAmount != null)
        {
            _targets.Add(new TargetProduction(TargetProductionTypes.MaxAmount, (float) maxAmount));
            if (Amount > (float) maxAmount) Amount = (float) maxAmount;
        }
    }
    public void ClearTargets() 
    {
        _targets.Clear();
    }
}