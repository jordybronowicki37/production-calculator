using productionCalculatorLib.components.targets;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasRecipe: INode
{
    Guid RecipeId { get; set; }
    PowerUp? PowerUp { get; set; }
}
