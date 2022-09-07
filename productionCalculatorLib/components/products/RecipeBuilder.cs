using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.products;

public class RecipeBuilder
{
    private readonly Worksheet _worksheet;
    private readonly string _name;

    private List<ThroughPut> _inputs = new();
    private List<ThroughPut> _outputs = new();

    public RecipeBuilder(Worksheet worksheet, string name)
    {
        _worksheet = worksheet;
        _name = name;
    }

    public RecipeBuilder AddInput(Product product, float amount)
    {
        _inputs.Add(new ThroughPut(product, amount));
        return this;
    }

    public RecipeBuilder AddOutput(Product product, float amount)
    {
        _outputs.Add(new ThroughPut(product, amount));
        return this;
    }

    public Recipe Build()
    {
        var recipe = _worksheet.GenerateRecipe(_name);

        foreach (var input in _inputs) recipe.InputThroughPuts.Add(input);
        foreach (var output in _outputs) recipe.OutputThroughPuts.Add(output);

        return recipe;
    }
}