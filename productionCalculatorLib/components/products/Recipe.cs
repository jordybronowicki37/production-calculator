namespace productionCalculatorLib.components.products;

public class Recipe
{
    public long Id { get; set; }
    public string Name { get; set; }

    public virtual List<ThroughPut> InputThroughPuts { get; private set; } = new();
    public virtual List<ThroughPut> OutputThroughPuts { get; private set; } = new();

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