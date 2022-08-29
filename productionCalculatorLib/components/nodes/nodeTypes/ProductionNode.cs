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