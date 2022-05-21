using productionCalculatorLib.components.nodes.enums;
using productionCalculatorLib.components.nodes.interfaces;

namespace productionCalculatorLib.components.nodes.nodeTypes;

public class SpawnNode: INodeOut
{
    public NodeTypes NodeType => NodeTypes.Spawn;

    private readonly List<INode> _outputNodes = new();
    public ThroughPut ThroughPut { get; set; }

    public SpawnNode(ThroughPut throughPut)
    {
        ThroughPut = throughPut;
    }

    public IList<INode> OutputNodes { get; }
    
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