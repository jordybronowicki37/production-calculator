namespace productionCalculatorLib.components.nodes.interfaces;

public interface IHasRecipe
{
    Recipe Recipe { get; set; }
    int ProductionAmount { get; set; }
}