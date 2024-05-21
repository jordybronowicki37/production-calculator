using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.calculator.linkedDomain;

public interface ILinkedHasRecipe: IHasRecipe
{
    Recipe Recipe { get; }
    Machine Machine { get; }
}
