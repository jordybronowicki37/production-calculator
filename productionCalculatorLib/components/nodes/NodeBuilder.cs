using productionCalculatorLib.components.connections;
using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.nodes.nodeTypes;
using productionCalculatorLib.components.products;
using productionCalculatorLib.components.worksheet;

namespace productionCalculatorLib.components.nodes;

public class NodeBuilder
{
    private NodeTypes _type;
    private Worksheet _worksheet;
    private Product _product;
    private Recipe _recipe;
    private List<ConnectionPlaceholder> _inputNodes = new();
    private List<ConnectionPlaceholder> _outputNodes = new();

    public NodeBuilder(Worksheet worksheet, NodeTypes type)
    {
        _worksheet = worksheet;
        _type = type;
    }

    public NodeBuilder SetProduct(Product product)
    {
        _product = product;
        return this;
    }

    public NodeBuilder SetRecipe(Recipe recipe)
    {
        _recipe = recipe;
        return this;
    }

    public NodeBuilder AddInputNode(INodeOut node, Product product)
    {
        _inputNodes.Add(new ConnectionPlaceholder(node, product));
        return this;
    }

    public NodeBuilder AddOutputNode(INodeIn node, Product product)
    {
        _outputNodes.Add(new ConnectionPlaceholder(node, product));
        return this;
    }

    public INode Build()
    {
        switch (_type)
        {
            case NodeTypes.Spawn:
                var spawnNode = new SpawnNode(_worksheet.NextNodeId, _product);
                _outputNodes.ForEach(o => CreateOutputConnection(spawnNode, o));
                _worksheet.AddNode(spawnNode);
                return spawnNode;
            case NodeTypes.Production:
                var productionNode = new ProductionNode(_worksheet.NextNodeId, _recipe);
                _inputNodes.ForEach(i => CreateInputConnection(productionNode, i));
                _outputNodes.ForEach(o => CreateOutputConnection(productionNode, o));
                _worksheet.AddNode(productionNode);
                return productionNode;
            case NodeTypes.End:
                var endNode = new EndNode(_worksheet.NextNodeId, _product);
                _inputNodes.ForEach(i => CreateInputConnection(endNode, i));
                _worksheet.AddNode(endNode);
                return endNode;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void CreateInputConnection(INodeIn nodeIn, ConnectionPlaceholder c)
    {
        if (c.OtherNode is not INodeOut otherNode) return;
        var connection = new Connection(otherNode, nodeIn, c.Product);
        if (!nodeIn.InputConnections.Contains(connection) && !otherNode.OutputConnections.Contains(connection))
        {
            nodeIn.AddInputConnection(connection);
            otherNode.AddOutputConnection(connection);
        }
    }

    private void CreateOutputConnection(INodeOut nodeOut, ConnectionPlaceholder c)
    {
        if (c.OtherNode is not INodeIn otherNode) return;
        var connection = new Connection(nodeOut, otherNode, c.Product);
        if (!nodeOut.OutputConnections.Contains(connection) && !otherNode.InputConnections.Contains(connection))
        {
            nodeOut.AddOutputConnection(connection);
            otherNode.AddInputConnection(connection);
        }
    }

    private struct ConnectionPlaceholder
    {
        public INode OtherNode;
        public Product Product;

        public ConnectionPlaceholder(INode otherNode, Product product)
        {
            OtherNode = otherNode;
            Product = product;
        }
    }
}