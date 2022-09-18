using System.Collections.ObjectModel;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.entityContainer;

public class EntityContainer
{
    public long Id { get; set; }
    
    public EntityContainer() {}

    private readonly List<Product> _products = new();
    public IList<Product> Products => new ReadOnlyCollection<Product>(_products);
    public Product GetOrGenerateProduct(string name)
    {
        var existingProduct = GetProduct(name);
        if (existingProduct != null) return existingProduct;

        var p = new Product(name);
        _products.Add(p);
        return p;
    }
    public Product? GetProduct(string name)
    {
        return _products.FirstOrDefault(r => r.Name == name);
    }
    public void RemoveProduct(string name)
    {
        var product = _products.FirstOrDefault(p => p.Name == name);
        if (product == null) return;
        _products.Remove(product);
    }

    private readonly List<Recipe> _recipes = new();
    public IList<Recipe> Recipes => new ReadOnlyCollection<Recipe>(_recipes);
    public Recipe GenerateRecipe(string name)
    {
        if (_recipes.Any(p => p.Name == name)) throw new Exception("Recipe already exists");
        var r = new Recipe(name);
        _recipes.Add(r);
        return r;
    }
    public RecipeBuilder GetRecipeBuilder(string name)
    {
        if (_recipes.Any(p => p.Name == name)) throw new Exception("Recipe already exists");
        return new RecipeBuilder(this, name);
    }
    public Recipe? GetRecipe(string name)
    {
        return _recipes.FirstOrDefault(r => r.Name == name);
    }
}