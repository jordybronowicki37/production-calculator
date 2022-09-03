using System.Collections.ObjectModel;
using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: INodeInOut, IHasRecipe
{
    public long Id { get; } = IdGenerators.NodeId;
    public Recipe Recipe { get; set; } = null!;
    public float ProductionAmount { get; set; }
    
    public ProductionNode() {}
    
    public ProductionNode(Recipe recipe)
    {
        Recipe = recipe;
    }

    private readonly List<Connection> _inputConnections = new();
    private readonly List<Connection> _outputConnections = new();
    public IList<Connection> InputConnections => new ReadOnlyCollection<Connection>(_inputConnections);
    public IList<Connection> OutputConnections => new ReadOnlyCollection<Connection>(_outputConnections);
    public void AddInputConnection(Connection connection)
    {
        if (!_inputConnections.Contains(connection))_inputConnections.Add(connection);
    }
    public void AddOutputConnection(Connection connection)
    {
        if (!_outputConnections.Contains(connection))_outputConnections.Add(connection);
    }
    public void RemoveConnnection(long connectionId)
    {
        {
            var connection = _inputConnections.Find(c => c.Id == connectionId);
            if (connection == null) return;
            _inputConnections.Remove(connection);
        }
        {
            var connection = _outputConnections.Find(c => c.Id == connectionId);
            if (connection == null) return;
            _outputConnections.Remove(connection);
            
        }
    }
    public void ClearConnections()
    {
        foreach (var inCon in InputConnections)
        {
            inCon.NodeIn.RemoveConnnection(inCon.Id);
        }
        foreach (var outCon in OutputConnections)
        {
            outCon.NodeOut.RemoveConnnection(outCon.Id);
        }
        InputConnections.Clear();
        OutputConnections.Clear();
    }

    private readonly List<TargetProduction> _targets = new();
    public IEnumerable<TargetProduction> ProductionTargets => new ReadOnlyCollection<TargetProduction>(_targets);
    public void SetExactTarget(float amount) 
    {
        ClearTargets();
        _targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        ProductionAmount = amount;
    }
    public void SetMinMaxTarget(float? minAmount, float? maxAmount) 
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
    public void ClearTargets() 
    {
        _targets.Clear();
    }
}