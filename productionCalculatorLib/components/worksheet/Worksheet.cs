using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public string Name { get; set; } = "";
    private int _nextNodeId;
    public int NextNodeId => _nextNodeId++;

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
        if (_products.Any(p => p.Name == name))
        {
            return _products.First(p => p.Name == name);
        }

        var p = new Product(name);
        _products.Add(p);
        return p;
    }
    public void RemoveProduct(string name)
    {
        var product = _products.First(p => p.Name == name);
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

    public Worksheet()
    {
    }
    
    public NodeBuilder GetNodeBuilder(NodeTypes type)
    {
        return new NodeBuilder(this, type);
    }
}