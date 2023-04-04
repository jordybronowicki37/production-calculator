using productionCalculatorLib.components.entityContainer;

namespace productionCalculatorLib.components.products;

public class RecipeBuilder
{
    private readonly EntityContainer _container;
    private readonly string _name;
    private readonly Machine _machine1;
    private readonly Machine[] _machines;
    private List<ThroughPut> _inputs = new();
    private List<ThroughPut> _outputs = new();

    public RecipeBuilder(EntityContainer container, string name, Machine machine1, params Machine[] machines)
    {
        _container = container;
        _name = name;
        _machine1 = machine1;
        _machines = machines;
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
        var recipe = _container.GenerateRecipe(_name, _machine1, _machines);

        foreach (var input in _inputs) recipe.InputThroughPuts.Add(input);
        foreach (var output in _outputs) recipe.OutputThroughPuts.Add(output);

        return recipe;
    }
}