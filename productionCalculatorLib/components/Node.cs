using System.Collections.ObjectModel;

namespace productionCalculatorLib.components;

public class Node
{
    private Recipe Recipe { get; set; }

    private readonly List<Node> _inputNodes = new();
    private readonly List<Node> _outputNodes = new();

    public IList<Node> InputNodes => new ReadOnlyCollection<Node>(_inputNodes);
    public IList<Node> OutputNodes => new ReadOnlyCollection<Node>(_outputNodes);

    public void AddInputNode(Node node)
    {
        if (!_inputNodes.Contains(node))
        {
            _inputNodes.Add(node);
        }
    }
    
    public void AddOutputNode(Node node)
    {
        if (!_outputNodes.Contains(node))
        {
            _outputNodes.Add(node);
        }
    }

    public void RemoveConnectedNode(Node node)
    {
        _inputNodes.Remove(node);
        _outputNodes.Remove(node);
    }

    public Node(Recipe recipe)
    {
        Recipe = recipe;
    }

    public override string ToString()
    {
        return $"Node{{}}";
    }
}