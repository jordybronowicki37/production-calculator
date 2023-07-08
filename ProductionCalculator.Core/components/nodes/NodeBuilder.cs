using productionCalculatorLib.components.entities;
using productionCalculatorLib.components.nodes.abstractions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.nodes;

public class NodeBuilder<TNodeType> where TNodeType : ANode, new()
{
    private readonly Worksheet _worksheet;
    private NodePosition _position = new NodePosition();
    private Product? _product;
    private Recipe? _recipe;
    private Machine? _machine;
    private float? _exactAmount = null;
    private float? _minAmount = null;
    private float? _maxAmount = null;

    public NodeBuilder(Worksheet worksheet)
    {
        _worksheet = worksheet;
    }
    
    public NodeBuilder<TNodeType> SetPosition(NodePosition position)
    {
        _position = position;
        return this;
    }

    public NodeBuilder<TNodeType> SetProduct(Product product)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(IHasProduct))) throw new InvalidOperationException("This node type does not support products");
        _product = product;
        return this;
    }

    public NodeBuilder<TNodeType> SetRecipe(Recipe recipe, Machine machine)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(IHasRecipe))) throw new InvalidOperationException("This node type does not support recipes");
        _recipe = recipe;
        _machine = machine;
        return this;
    }

    public NodeBuilder<TNodeType> SetExactTarget(float amount)
    {
        if (_minAmount == null && _maxAmount == null) _exactAmount = amount;
        return this;
    }

    public NodeBuilder<TNodeType> SetMinMaxTarget(float? minAmount, float? maxAmount)
    {
        if (_exactAmount == null)
        {
            _minAmount = minAmount;
            _maxAmount = maxAmount;
        }
        return this;
    }

    public TNodeType Build()
    {
        TNodeType newNode = new()
        {
            Position = _position
        };

        switch (newNode)
        {
            case IHasProduct when _product == null:
                throw new InvalidOperationException("No product set");
            
            case IHasRecipe when _recipe == null:
                throw new InvalidOperationException("No recipe set");
            
            case IHasRecipe when _machine == null:
                throw new InvalidOperationException("No machine set");
            
            case IHasRecipe when !_machine.Recipes.Contains(_recipe.Id):
                throw new InvalidOperationException("Machine does not provide recipe");
            
            case IHasProduct nodeProduct:
                nodeProduct.ProductId = _product.Id;
                break;
            
            case IHasRecipe nodeRecipe:
                nodeRecipe.RecipeId = _recipe.Id;
                nodeRecipe.MachineId = _machine.Id;
                break;
        }

        if (_exactAmount != null)
        {
            newNode.SetExactTarget((float) _exactAmount);
        } 
        else if (_minAmount != null || _maxAmount != null)
        {
            newNode.SetMinMaxTarget(_minAmount, _maxAmount);
        }

        _worksheet.AddNode(newNode);
        return newNode;
    }
}