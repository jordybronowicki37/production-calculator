using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: ANode, INodeInOut, IHasRecipe
{
    public Guid RecipeId { get; set; }
    public float ProductionAmount { get; set; }
    public override ICollection<TargetProduction> Targets { get; set; } = new List<TargetProduction>();
    
    public ProductionNode() {}
    
    public ProductionNode(Recipe recipe)
    {
        RecipeId = recipe.Id;
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