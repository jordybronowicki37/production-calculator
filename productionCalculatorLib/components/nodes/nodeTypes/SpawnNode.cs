using System.Collections.ObjectModel;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: ANodeOut, IHasProduct
{
    private readonly List<Connection> _outputConnections = new();
    private readonly List<TargetProduction> _targets = new();
    public Product Product { get; set; } = null!;
    public float Amount { get; set; }
    
    public SpawnNode() {}

    public SpawnNode(Product product)
    {
        Product = product;
    }

    public override IList<Connection> OutputConnections => new ReadOnlyCollection<Connection>(_outputConnections);
    public override void AddOutputConnection(Connection connection)
    {
        if (!_outputConnections.Contains(connection))_outputConnections.Add(connection);
    }
    public override void RemoveConnnection(long connectionId)
    {
        var connection = _outputConnections.Find(c => c.Id == connectionId);
        if (connection == null) return;
        _outputConnections.Remove(connection);
    }
    public override void ClearConnections()
    {
        foreach (var outCon in OutputConnections)
        {
            outCon.NodeOut.RemoveConnnection(outCon.Id);
        }
        _outputConnections.Clear();
    }
    
    public override IEnumerable<TargetProduction> ProductionTargets => new ReadOnlyCollection<TargetProduction>(_targets);
    public override void SetExactTarget(float amount) 
    {
        ClearTargets();
        _targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        Amount = amount;
    }
    public override void SetMinMaxTarget(float? minAmount, float? maxAmount) 
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
    public override void ClearTargets() 
    {
        _targets.Clear();
    }
}