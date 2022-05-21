using System.Collections.ObjectModel;
using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class ProductionNode: INodeInOut
{
    public NodeTypes NodeType => NodeTypes.Production;

    public Recipe Recipe { get; set; }
    
    public ProductionNode(Recipe recipe)
    {
        Recipe = recipe;
    }

    // Nodes
    private readonly List<INode> _inputNodes = new();
    private readonly List<INode> _outputNodes = new();

    public IList<INode> InputNodes => new ReadOnlyCollection<INode>(_inputNodes);
    public IList<INode> OutputNodes => new ReadOnlyCollection<INode>(_outputNodes);

    public void AddInputNode(INodeOut node)
    {
        if (!_inputNodes.Contains(node))_inputNodes.Add(node);
        if (!node.OutputNodes.Contains(this))node.AddOutputNode(this);
    }
    
    public void AddOutputNode(INodeIn node)
    {
        if (!_outputNodes.Contains(node))_outputNodes.Add(node);
        if (!node.InputNodes.Contains(this)) node.AddInputNode(this);
    }
    
    public void RemoveConnectedNode(INode node)
    {
        _inputNodes.Remove(node);
        _outputNodes.Remove(node);
    }

    public override string ToString()
    {
        return $"Node{{Input: {_inputNodes}}}";
    }
}