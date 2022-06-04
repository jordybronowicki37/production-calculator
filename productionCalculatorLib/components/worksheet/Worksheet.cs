using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public string Name { get; set; } = "";
    
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
        foreach (var n in _nodes)
        {
            n.RemoveConnectedNode(node);
        }
    }
    
    private readonly List<Product> _products = new();
    public IList<Product> Products => new ReadOnlyCollection<Product>(_products);
    public Product GetOrGenerateProduct(string name)
    {
        if (_products.Any(p => p.Name == name))
        {
            return _products.First(p => p.Name == name);
        }

        var p = new Product(name);
        _products.Add(p);
        return p;
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

    public Worksheet()
    {
    }
    
    
}