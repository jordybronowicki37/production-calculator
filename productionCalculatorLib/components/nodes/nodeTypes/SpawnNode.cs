using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: ANode, INodeOut, IHasProduct
{
    public virtual Product Product { get; set; } = null!;
    public float Amount { get; set; }
    public override ICollection<TargetProduction> Targets { get; set; } = new List<TargetProduction>();
    
    public SpawnNode() {}

    public SpawnNode(Product product)
    {
        Product = product;
    }

    public override void SetExactTarget(float amount) 
    {
        ClearTargets();
        Targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        Amount = amount;
    }
    
    public override void SetMinMaxTarget(float? minAmount, float? maxAmount) 
    {
        ClearTargets();
        if (minAmount != null)
        {
            Targets.Add(new TargetProduction(TargetProductionTypes.MinAmount, (float) minAmount));
            Amount = (float) minAmount;
        }
        if (maxAmount != null)
        {
            Targets.Add(new TargetProduction(TargetProductionTypes.MaxAmount, (float) maxAmount));
            if (Amount > (float) maxAmount) Amount = (float) maxAmount;
        }
    }
    
    public override void ClearTargets() 
    {
        Targets.Clear();
    }
}