using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: ANode, INodeInOut, IHasRecipe
{
    public virtual Recipe Recipe { get; set; } = null!;
    public float ProductionAmount { get; set; }
    
    public ProductionNode() {}
    
    public ProductionNode(Recipe recipe)
    {
        Recipe = recipe;
    }
    
    public override IList<Connection> InputConnections { get; } = new List<Connection>();
    public override IList<Connection> OutputConnections { get; } = new List<Connection>();
    public override void AddInputConnection(Connection connection)
    {
        if (!InputConnections.Contains(connection))InputConnections.Add(connection);
    }
    public override void AddOutputConnection(Connection connection)
    {
        if (!OutputConnections.Contains(connection))OutputConnections.Add(connection);
    }
    public override void RemoveConnnection(long connectionId)
    {
        {
            var connection = InputConnections.FirstOrDefault(c => c.Id == connectionId);
            if (connection != null) InputConnections.Remove(connection);
        }
        {
            var connection = OutputConnections.FirstOrDefault(c => c.Id == connectionId);
            if (connection != null) OutputConnections.Remove(connection);
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
        InputConnections.Clear();
        OutputConnections.Clear();
    }

    public override IList<TargetProduction> ProductionTargets { get; set; } = new List<TargetProduction>();
    public override void SetExactTarget(float amount) 
    {
        ClearTargets();
        ProductionTargets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        ProductionAmount = amount;
    }
    public override void SetMinMaxTarget(float? minAmount, float? maxAmount) 
    {
        ClearTargets();
        if (minAmount != null)
        {
            ProductionTargets.Add(new TargetProduction(TargetProductionTypes.MinAmount, (float) minAmount));
            ProductionAmount = (float) minAmount;
        }
        if (maxAmount != null)
        {
            ProductionTargets.Add(new TargetProduction(TargetProductionTypes.MaxAmount, (float) maxAmount));
            if (ProductionAmount > (float) maxAmount) ProductionAmount = (float) maxAmount;
        }
    }
    public override void ClearTargets() 
    {
        ProductionTargets.Clear();
    }
}