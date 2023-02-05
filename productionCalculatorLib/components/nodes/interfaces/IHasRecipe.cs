using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasRecipe: INode
{
    Guid RecipeId { get; set; }
    float ProductionAmount { get; set; }
}