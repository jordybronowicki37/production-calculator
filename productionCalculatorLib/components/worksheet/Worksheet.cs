using System.Collections.ObjectModel;
using productionCalculatorLib.components.entityContainer;
using productionCalculatorLib.components.nodes;
using productionCalculatorLib.components.nodes.abstractions;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public Worksheet() {}

    public long Id { get; set; }
    public string Name { get; set; } = "";
    public EntityContainer EntityContainer { get; } = new();

    public bool CalculationSucceeded { get; set; } = true;
    public string CalculationError { get; set; } = "";

    private readonly List<ANode> _nodes = new();
    public IList<ANode> Nodes => new ReadOnlyCollection<ANode>(_nodes);
    public void AddNode(ANode node)
    {
        if (!_nodes.Contains(node))
        {
            _nodes.Add(node);
        }
    }
    public void RemoveNode(ANode node)
    {
        _nodes.Remove(node);
        node.ClearConnections();
    }

    public NodeBuilder<TNodeType> GetNodeBuilder<TNodeType>() where TNodeType : ANode, new()
    {
        return new NodeBuilder<TNodeType>(this);
    }
}