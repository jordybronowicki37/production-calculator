using ProductionCalculator.Core.components.entities;
using ProductionCalculator.Core.components.entityContainer;
using ProductionCalculator.Core.components.nodes.nodeTypes;

namespace ProductionCalculator.Core.components.calculator.linkedDomain;

public sealed class LinkedProductionNode: ProductionNode, ILinkedHasRecipe, ILinkedNodeIn, ILinkedNodeOut
{
    public Recipe Recipe { get; }
    public Machine Machine { get; }
    public ICollection<LinkedConnection> InConnections { get; } = new List<LinkedConnection>();
    public ICollection<LinkedConnection> OutConnections { get; } = new List<LinkedConnection>();

    public LinkedProductionNode(EntityContainer entityContainer, ProductionNode productionNode)
    {
        Id = productionNode.Id;
        Amount = productionNode.Amount;
        Position = productionNode.Position;
        Targets = productionNode.Targets;
        RecipeId = productionNode.RecipeId;
        Recipe = entityContainer.GetRecipe(RecipeId)!;
        MachineId = productionNode.MachineId;
        Machine = entityContainer.GetMachine(RecipeId)!;
        PowerUp = productionNode.PowerUp;
    }
}