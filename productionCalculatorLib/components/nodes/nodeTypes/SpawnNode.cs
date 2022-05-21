using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: INodeOut, IHasProduct
{
    public NodeTypes NodeType => NodeTypes.Spawn;

    private readonly List<INode> _outputNodes = new();
    public Product Product { get; set; }
    public int Amount { get; set; }

    public SpawnNode(Product product, int amount)
    {
        Product = product;
        Amount = amount;
    }

    public IList<INode> OutputNodes => new ReadOnlyCollection<INode>(_outputNodes);
    
    public void AddOutputNode(INodeIn node)
    {
        if (!_outputNodes.Contains(node))_outputNodes.Add(node);
        if (!node.InputNodes.Contains(this)) node.AddInputNode(this);
    }
    
    public void RemoveConnectedNode(INode node)
    {
        _outputNodes.Remove(node);
    }
}