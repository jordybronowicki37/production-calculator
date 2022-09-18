using System.Collections.ObjectModel;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: ANodeIn, IHasProduct
{
    private readonly List<Connection> _inputConnections = new();
    private readonly List<TargetProduction> _targets = new();
    public Product Product { get; set; } = null!;
    public float Amount { get; set; }
    
    public EndNode() {}

    public EndNode(Product product)
    {
        Product = product;
    }

    public override IList<Connection> InputConnections => new ReadOnlyCollection<Connection>(_inputConnections);
    public override void AddInputConnection(Connection connection)
    {
        if (!_inputConnections.Contains(connection)) _inputConnections.Add(connection);
    }
    public override void RemoveConnnection(long connectionId)
    {
        var connection = _inputConnections.Find(c => c.Id == connectionId);
        if (connection == null) return;
        _inputConnections.Remove(connection);
    }
    public override void ClearConnections()
    {
        foreach (var inCon in InputConnections)
        {
            inCon.NodeIn.RemoveConnnection(inCon.Id);
        }
        _inputConnections.Clear();
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