using System.Collections.ObjectModel;
using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: INodeIn, IHasProduct
{
    public long Id { get; } = IdGenerators.NodeId;
    private readonly List<Connection> _inputConnections = new();
    private readonly List<TargetProduction> _targets = new();
    public Product Product { get; set; } = null!;
    public float Amount { get; set; }
    
    public EndNode() {}

    public EndNode(Product product)
    {
        Product = product;
    }

    public IList<Connection> InputConnections => new ReadOnlyCollection<Connection>(_inputConnections);
    public void AddInputConnection(Connection connection)
    {
        if (!_inputConnections.Contains(connection)) _inputConnections.Add(connection);
    }
    public void RemoveConnnection(long connectionId)
    {
        var connection = _inputConnections.Find(c => c.Id == connectionId);
        if (connection == null) return;
        _inputConnections.Remove(connection);
    }
    public void ClearConnections()
    {
        foreach (var inCon in InputConnections)
        {
            inCon.NodeIn.RemoveConnnection(inCon.Id);
        }
        InputConnections.Clear();
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