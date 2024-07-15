using ProductionCalculator.Core.components.entities;
using ProductionCalculator.Core.components.nodes.interfaces;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

public interface ILinkedHasRecipe: IHasRecipe
{
    Recipe Recipe { get; }
    Machine Machine { get; }
}
