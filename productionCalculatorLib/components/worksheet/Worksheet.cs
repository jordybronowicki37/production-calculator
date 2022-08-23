using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public long Id { get; } = IdGenerators.WorksheetId;
    public string Name { get; set; } = "";

    public bool CalculationSucceeded { get; set; }
    public string CalculationError { get; set; }

    private readonly List<INode> _nodes = new();
    public IList<INode> Nodes => new ReadOnlyCollection<INode>(_nodes);
    public void AddNode(INode node)
    {
        if (!_nodes.Contains(node))
        {
            _nodes.Add(node);
        }
    }
    public void RemoveNode(INode node)
    {
        _nodes.Remove(node);
        // TODO remove connections
    }
    
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
    public Recipe? GetRecipe(string name)
    {
        return _recipes.FirstOrDefault(r => r.Name == name);
    }

    public Worksheet()
    {
    }
    
    public NodeBuilder<TNodeType> GetNodeBuilder<TNodeType>() where TNodeType : INode, new()
    {
        return new NodeBuilder<TNodeType>(this);
    }
}