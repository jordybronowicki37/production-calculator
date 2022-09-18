namespace productionCalculatorLib.components.products;

public class Recipe
{
    public long Id { get; set; }
    public string Name { get; }

    public virtual List<ThroughPut> InputThroughPuts { get; } = new();
    public virtual List<ThroughPut> OutputThroughPuts { get; } = new();

    public Recipe() {}

    public Recipe(string name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return $"Recipe:{{Input:{InputThroughPuts}, Output:{OutputThroughPuts}}}";
    }
}