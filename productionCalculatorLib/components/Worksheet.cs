using System.Collections.ObjectModel;

namespace productionCalculatorLib.components;

public class Worksheet
{
    public string Name { get; set; } = "";
    
    private readonly List<Node> _nodes = new();

    public IList<Node> Nodes => new ReadOnlyCollection<Node>(_nodes);

    public void AddNode(Node node)
    {
        if (!_nodes.Contains(node))
        {
            _nodes.Add(node);
        }
    }

    public void RemoveNode(Node node)
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