using productionCalculatorLib.components.calculator.targets;
using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.nodes;

public class NodeBuilder<TNodeType> where TNodeType : INode, new()
{
    private readonly Worksheet _worksheet;
    private Product? _product;
    private Recipe? _recipe;
    private readonly List<ConnectionPlaceholder<INodeOut>> _inputNodes = new();
    private readonly List<ConnectionPlaceholder<INodeIn>> _outputNodes = new();
    private float? _exactAmount = null;
    private float? _minAmount = null;
    private float? _maxAmount = null;

    public NodeBuilder(Worksheet worksheet)
    {
        _worksheet = worksheet;
    }

    public NodeBuilder<TNodeType> SetProduct(Product product)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(IHasProduct))) throw new InvalidOperationException("This node type does not support products");
        _product = product;
        return this;
    }

    public NodeBuilder<TNodeType> SetRecipe(Recipe recipe)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(IHasRecipe))) throw new InvalidOperationException("This node type does not support recipes");
        _recipe = recipe;
        return this;
    }

    public NodeBuilder<TNodeType> AddInputNode(INodeOut node, Product product)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(INodeIn))) throw new InvalidOperationException("This node type does not support inputs");
        _inputNodes.Add(new ConnectionPlaceholder<INodeOut>(node, product));
        return this;
    }

    public NodeBuilder<TNodeType> AddOutputNode(INodeIn node, Product product)
    {
        if (!typeof(TNodeType).GetInterfaces().Contains(typeof(INodeOut))) throw new InvalidOperationException("This node type does not support outputs");
        _outputNodes.Add(new ConnectionPlaceholder<INodeIn>(node, product));
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
        TNodeType newNode = new();
        
        if (newNode is IHasProduct nodeProduct)
        {
            nodeProduct.Product = _product ?? throw new InvalidOperationException("No product set");
        }
        
        if (newNode is IHasRecipe nodeRecipe)
        {
            nodeRecipe.Recipe = _recipe ?? throw new InvalidOperationException("No recipe set");
        }
        
        if (newNode is INodeIn nodeIn)
        {
            _inputNodes.ForEach(connectionPlaceholder =>
            {
                var otherNode = connectionPlaceholder.OtherNode;
                var connection = new Connection(otherNode, nodeIn, connectionPlaceholder.Product);
                if (nodeIn.InputConnections.Contains(connection) ||
                    otherNode.OutputConnections.Contains(connection)) return;
                nodeIn.AddInputConnection(connection);
                otherNode.AddOutputConnection(connection);
            });
        }

        if (newNode is INodeOut nodeOut)
        {
            _outputNodes.ForEach(connectionPlaceholder =>
            {
                var otherNode = connectionPlaceholder.OtherNode;
                var connection = new Connection(nodeOut, otherNode, connectionPlaceholder.Product);
                if (nodeOut.OutputConnections.Contains(connection) ||
                    otherNode.InputConnections.Contains(connection)) return;
                nodeOut.AddOutputConnection(connection);
                otherNode.AddInputConnection(connection);
            });
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

    private struct ConnectionPlaceholder<T>
    {
        public readonly T OtherNode;
        public readonly Product Product;

        public ConnectionPlaceholder(T otherNode, Product product)
        {
            OtherNode = otherNode;
            Product = product;
        }
    }
}