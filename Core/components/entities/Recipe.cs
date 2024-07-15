namespace ProductionCalculator.Core.components.entities;

public class Recipe
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public virtual List<ThroughPut> InputThroughPuts { get; private set; } = new();
    public virtual List<ThroughPut> OutputThroughPuts { get; private set; } = new();

    public Recipe() {}

    public override string ToString()
    {
        return $"Recipe:{{Input:{InputThroughPuts}, Output:{OutputThroughPuts}}}";
    }
}