using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: ANode, INodeIn, IHasProduct
{
    public virtual Product Product { get; set; } = null!;
    public float Amount { get; set; }
    
    public EndNode() {}

    public EndNode(Product product)
    {
        Product = product;
    }

    public override IList<TargetProduction> ProductionTargets { get; set; } = new List<TargetProduction>();
    public override void SetExactTarget(float amount) 
    {
        ClearTargets();
        ProductionTargets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        Amount = amount;
    }
    public override void SetMinMaxTarget(float? minAmount, float? maxAmount) 
    {
        ClearTargets();
        if (minAmount != null)
        {
            ProductionTargets.Add(new TargetProduction(TargetProductionTypes.MinAmount, (float) minAmount));
            Amount = (float) minAmount;
        }
        if (maxAmount != null)
        {
            ProductionTargets.Add(new TargetProduction(TargetProductionTypes.MaxAmount, (float) maxAmount));
            if (Amount > (float) maxAmount) Amount = (float) maxAmount;
        }
    }
    public override void ClearTargets() 
    {
        ProductionTargets.Clear();
    }
}