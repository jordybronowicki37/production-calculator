using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.abstractions;

public abstract class ANode: INode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public float Amount { get; set; }
    public virtual ICollection<TargetProduction> Targets { get; set; } = new List<TargetProduction>();
    
    public virtual void SetExactTarget(float amount) 
    {
        ClearTargets();
        Targets.Add(new TargetProduction(TargetProductionTypes.ExactAmount, amount));
        Amount = amount;
    }
    
    public virtual void SetMinMaxTarget(float? minAmount, float? maxAmount) 
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
    
    public virtual void ClearTargets() 
    {
        Targets.Clear();
    }
}