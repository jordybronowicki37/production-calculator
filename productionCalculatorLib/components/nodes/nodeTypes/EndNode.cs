using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: ANode, INodeIn, IHasProduct
{
    public Guid ProductId { get; set; }
    public float Amount { get; set; }
    public override ICollection<TargetProduction> Targets { get; set; } = new List<TargetProduction>();
    
    public EndNode() {}

    public EndNode(Product product)
    {
        ProductId = product.Id;
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