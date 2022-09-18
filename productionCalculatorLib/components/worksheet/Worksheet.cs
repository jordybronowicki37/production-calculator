using System.Collections.ObjectModel;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public Worksheet() {}

    public long Id { get; set; }
    public string Name { get; set; } = "";
    public EntityContainer EntityContainer { get; } = new();

    public bool CalculationSucceeded { get; set; } = true;
    public string CalculationError { get; set; } = "";

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
        node.ClearConnections();
    }

    public NodeBuilder<TNodeType> GetNodeBuilder<TNodeType>() where TNodeType : INode, new()
    {
        return new NodeBuilder<TNodeType>(this);
    }
}