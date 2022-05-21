using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.worksheet;

public class Worksheet
{
    public string Name { get; set; } = "";
    
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
        foreach (var n in _nodes)
        {
            n.RemoveConnectedNode(node);
        }
    }

    public Worksheet()
    {
    }
    
    
}