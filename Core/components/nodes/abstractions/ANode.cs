using ProductionCalculator.Core.components.nodes.interfaces;
using ProductionCalculator.Core.components.targets;

namespace ProductionCalculator.Core.components.nodes.abstractions;

public abstract class ANode: INode
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public float Amount { get; set; }
    public NodePosition Position { get; set; } = new NodePosition();
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