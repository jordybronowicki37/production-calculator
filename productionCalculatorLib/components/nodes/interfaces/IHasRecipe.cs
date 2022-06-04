using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasRecipe
{
    Recipe Recipe { get; set; }
    float ProductionAmount { get; set; }
}