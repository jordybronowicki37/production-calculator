using System.Collections.ObjectModel;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: ANodeInOut, IHasRecipe
{
    public Recipe Recipe { get; set; } = null!;
    public float ProductionAmount { get; set; }
    
    public ProductionNode() {}
    
    public ProductionNode(Recipe recipe)
    {
        Recipe = recipe;
    }

    private readonly List<Connection> _inputConnections = new();
    private readonly List<Connection> _outputConnections = new();
    public override IList<Connection> InputConnections => new ReadOnlyCollection<Connection>(_inputConnections);
    public override IList<Connection> OutputConnections => new ReadOnlyCollection<Connection>(_outputConnections);
    public override void AddInputConnection(Connection connection)
    {
        if (!_inputConnections.Contains(connection))_inputConnections.Add(connection);
    }
    public override void AddOutputConnection(Connection connection)
    {
        if (!_outputConnections.Contains(connection))_outputConnections.Add(connection);
    }
    public override void RemoveConnnection(long connectionId)
    {
        {
            var connection = _inputConnections.Find(c => c.Id == connectionId);
            if (connection != null) _inputConnections.Remove(connection);
        }
        {
            var connection = _outputConnections.Find(c => c.Id == connectionId);
            if (connection != null) _outputConnections.Remove(connection);
        }
    }
    public override void ClearConnections()
    {
        foreach (var inCon in InputConnections)
        {
            inCon.NodeIn.RemoveConnnection(inCon.Id);
        }
        foreach (var outCon in OutputConnections)
        {
            outCon.NodeOut.RemoveConnnection(outCon.Id);
        }
        _inputConnections.Clear();
        _outputConnections.Clear();
    }

    private readonly List<TargetProduction> _targets = new();
    public override IEnumerable<TargetProduction> ProductionTargets => new ReadOnlyCollection<TargetProduction>(_targets);
    public override void SetExactTarget(float amount) 
    {
        ClearTargets();
        _targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        ProductionAmount = amount;
    }
    public override void SetMinMaxTarget(float? minAmount, float? maxAmount) 
    {
        ClearTargets();
        if (minAmount != null)
        {
            _targets.Add(new TargetProduction(TargetProductionTypes.MinAmount, (float) minAmount));
            ProductionAmount = (float) minAmount;
        }
        if (maxAmount != null)
        {
            _targets.Add(new TargetProduction(TargetProductionTypes.MaxAmount, (float) maxAmount));
            if (ProductionAmount > (float) maxAmount) ProductionAmount = (float) maxAmount;
        }
    }
    public override void ClearTargets() 
    {
        _targets.Clear();
    }
}