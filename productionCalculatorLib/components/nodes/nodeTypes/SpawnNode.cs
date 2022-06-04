using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: INodeOut, IHasProduct
{
    private readonly List<INode> _outputNodes = new();
    public Product Product { get; set; }
    public float Amount { get; set; }

    public SpawnNode(Product product)
    {
        Product = product;
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