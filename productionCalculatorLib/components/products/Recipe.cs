namespace productionCalculatorLib.components.products;

public class Recipe
{
    public long Id { get; } = IdGenerators.RecipeId;
    public string Name { get; }

    public List<ThroughPut> InputThroughPuts { get; } = new();
    public List<ThroughPut> OutputThroughPuts { get; } = new();

    public Recipe(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"Recipe:{{Input:{InputThroughPuts}, Output:{OutputThroughPuts}}}";
    }
}