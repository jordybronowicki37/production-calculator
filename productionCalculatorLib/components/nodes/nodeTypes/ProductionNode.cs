using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: ANode, INodeInOut, IHasRecipe
{
    public virtual Recipe Recipe { get; set; } = null!;
    public float ProductionAmount { get; set; }
    public override ICollection<TargetProduction> Targets { get; set; } = new List<TargetProduction>();
    
    public ProductionNode() {}
    
    public ProductionNode(Recipe recipe)
    {
        Recipe = recipe;
    }

    public override void SetExactTarget(float amount) 
    {
        ClearTargets();
        Targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        ProductionAmount = amount;
    }
    
    public override void SetMinMaxTarget(float? minAmount, float? maxAmount) 
    {
        ClearTargets();
        if (minAmount != null)
        {
            Targets.Add(new TargetProduction(TargetProductionTypes.MinAmount, (float) minAmount));
            ProductionAmount = (float) minAmount;
        }
        if (maxAmount != null)
        {
            Targets.Add(new TargetProduction(TargetProductionTypes.MaxAmount, (float) maxAmount));
            if (ProductionAmount > (float) maxAmount) ProductionAmount = (float) maxAmount;
        }
    }
    
    public override void ClearTargets() 
    {
        Targets.Clear();
    }
}