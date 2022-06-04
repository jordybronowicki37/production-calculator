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
    private List<INodeOut> _inputNodes = new();
    private List<INodeIn> _outputNodes = new();

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

    public NodeBuilder AddInputNode(INodeOut node)
    {
        _inputNodes.Add(node);
        return this;
    }

    public NodeBuilder AddOutputNode(INodeIn node)
    {
        _outputNodes.Add(node);
        return this;
    }

    public INode Build()
    {
        switch (_type)
        {
            case NodeTypes.Spawn:
                var spawnNode = new SpawnNode(_worksheet.NextNodeId, _product);
                _outputNodes.ForEach(spawnNode.AddOutputNode);
                _worksheet.AddNode(spawnNode);
                return spawnNode;
            case NodeTypes.Production:
                var productionNode = new ProductionNode(_worksheet.NextNodeId, _recipe);
                _inputNodes.ForEach(productionNode.AddInputNode);
                _outputNodes.ForEach(productionNode.AddOutputNode);
                _worksheet.AddNode(productionNode);
                return productionNode;
            case NodeTypes.End:
                var endNode = new EndNode(_worksheet.NextNodeId, _product);
                _inputNodes.ForEach(endNode.AddInputNode);
                _worksheet.AddNode(endNode);
                return endNode;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}