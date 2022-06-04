using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes.exceptions;
using productionCalculatorLib.components.nodes.interfaces;
using productionCalculatorLib.components.products;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class EndNode: INodeIn, IHasProduct
{
    public int Id { get; }
    private readonly List<INode> _inputNodes = new();
    public Product Product { get; set; }
    public float Amount { get; set; }

    public EndNode(int id, Product product)
    {
        Id = id;
        Product = product;
    }

    public IList<INode> InputNodes => new ReadOnlyCollection<INode>(_inputNodes);
    
    public void AddInputNode(INodeOut node)
    {
        if (_inputNodes.Count == 0)
        {
            _inputNodes.Add(node);
        }
        else
        {
            throw new MaxConnectionsReachedException("Only 1 input connection is allowed on end-node");
        }
        if (!node.OutputNodes.Contains(this)) node.AddOutputNode(this);
    }
    
    public void RemoveConnectedNode(INode node)
    {
        _inputNodes.Remove(node);
    }
}