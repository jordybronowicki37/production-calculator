using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.entityContainer;

public class EntityContainer
{
    public long Id { get; set; }
    
    public EntityContainer() {}

    public ICollection<Product> Products { get; } = new List<Product>();
    public Product GetOrGenerateProduct(string name)
    {
        var existingProduct = GetProduct(name);
        if (existingProduct != null) return existingProduct;

        var p = new Product(name);
        Products.Add(p);
        return p;
    }
    public Product? GetProduct(string name)
    {
        return Products.FirstOrDefault(r => r.Name == name);
    }
    public void RemoveProduct(string name)
    {
        var product = Products.FirstOrDefault(p => p.Name == name);
        if (product == null) return;
        Products.Remove(product);
    }

    public ICollection<Recipe> Recipes { get; } = new List<Recipe>();
    public Recipe GenerateRecipe(string name)
    {
        if (Recipes.Any(p => p.Name == name)) throw new Exception("Recipe already exists");
        var r = new Recipe(name);
        Recipes.Add(r);
        return r;
    }
    public RecipeBuilder GetRecipeBuilder(string name)
    {
        if (Recipes.Any(p => p.Name == name)) throw new Exception("Recipe already exists");
        return new RecipeBuilder(this, name);
    }
    public Recipe? GetRecipe(string name)
    {
        return Recipes.FirstOrDefault(r => r.Name == name);
    }
}